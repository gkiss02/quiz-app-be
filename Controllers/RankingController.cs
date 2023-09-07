using DotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.Controllers;

[ApiController]
[Route("[controller]")]

public class RankingController : ControllerBase {
    DataContext _context;

    public RankingController(IConfiguration configuration) {
        _context = new DataContext(configuration);
    }

    [HttpGet("BestsOfAllTime")]
    public IEnumerable<UserWithScore> BestsOfAllTime () {
        string sql = $@"Select TOP 10 Users.UserID, Users.UserName, SUM(Score.Score) as Score FROM Score
                        inner join Users on Users.UserID = Score.UserID
                        group by Users.UserID, Users.UserName
                        order by SUM(Score.Score) desc;";

        IEnumerable<UserWithScore> users = _context.LoadData<UserWithScore>(sql);

        return users;
    }

    [HttpGet("BestsOfThisMonth")]
    public IEnumerable<UserWithScore> BestsOfThisMonth () {
        DateTime dateTime = DateTime.Now;
        string sql = $@"Select TOP 10 Users.UserID, Users.UserName, SUM(Score.Score) as Score FROM Score
                        inner join Users on Users.UserID = Score.UserID
                        where Month(DateOfPlay) = {dateTime.Month}
                        group by Users.UserID, Users.UserName
                        order by SUM(Score.Score) desc;";

        IEnumerable<UserWithScore> users = _context.LoadData<UserWithScore>(sql);

        return users;
    }

    [HttpGet("BestsOfThisWeek")]
    public IEnumerable<UserWithScore> BestsOfThisWeek () {
        DateTime dateTime = DateTime.Now;
        int dayOfWeek = (int) dateTime.DayOfWeek;
        string startOfWeek = dateTime.AddDays(-dayOfWeek + 1).ToString("yyyy-MM-dd");
        string endOfWeek = dateTime.AddDays(7 - dayOfWeek).ToString("yyyy-MM-dd");;

        string sql = $@"Select TOP 10 Users.UserID, Users.UserName, SUM(Score.Score) as Score FROM Score
                        inner join Users on Users.UserID = Score.UserID
                        where DateOfPlay between '{startOfWeek}' and '{endOfWeek}'
                        group by Users.UserID, Users.UserName
                        order by SUM(Score.Score) desc;";

        IEnumerable<UserWithScore> users = _context.LoadData<UserWithScore>(sql);

        return users;
    }

    [HttpGet("userRanking/{id}")]
    public UserRanking userRanking (int id) {
        string sql = $@"WITH RankedScores AS (
                        SELECT Users.UserID, Users.UserName, SUM(Score.Score) AS TotalScore, 
                        DENSE_RANK() OVER (ORDER BY SUM(Score.Score) DESC) AS UserRank
                        FROM Score
                        INNER JOIN Users ON Users.UserID = Score.UserID
                        GROUP BY Users.UserID, Users.UserName
                        )
                        SELECT UserID, UserName, TotalScore, UserRank FROM RankedScores
                        WHERE UserID = {id};";

        UserRanking user =  _context.LoadDataSingle<UserRanking>(sql);
        
        return user;
    }
}