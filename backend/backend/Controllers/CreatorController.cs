using backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Entities;


[ApiController]
[Route("api/[controller]")]
public class CreatorController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CreatorController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Creator
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Creator>>> GetCreators()
    {
        var creators = await _context.Creators
            .Select(c => new Creator
            {
                CreatorID = c.CreatorID,
                AccountID = c.AccountID,
                PaymentID = c.PaymentID,
                UserName = c.UserName,
                ProfilePicture = c.ProfilePicture,
                BackgroundPicture = c.BackgroundPicture,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Address = c.Address,
                Phone = c.Phone,
                LastLogDate = c.LastLogDate,
                AllowCommission = c.AllowCommission,
                Biography = c.Biography,
                VIP = c.VIP,
                FollowCounts = c.FollowCounts,

            })
            .ToListAsync();

        return creators;
    }


    [HttpGet("VipCreators")]
    public async Task<ActionResult<IEnumerable<Creator>>> GetVipCreators()
    {
        var vipCreators = await _context.Creators
            .Where(c => c.VIP == true)
            .Select(c => new Creator
            {
                CreatorID = c.CreatorID,
                AccountID = c.AccountID,
                PaymentID = c.PaymentID,
                UserName = c.UserName,
                ProfilePicture = c.ProfilePicture,
                BackgroundPicture = c.BackgroundPicture,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Address = c.Address,
                Phone = c.Phone,
                LastLogDate = c.LastLogDate,
                AllowCommission = c.AllowCommission,
                Biography = c.Biography,
                VIP = c.VIP,
                FollowCounts = c.FollowCounts,
            })
            .ToListAsync();

        return vipCreators;
    }


    [HttpGet("GetID/UserName/Vip")]
    public async Task<ActionResult<IEnumerable<Creator>>> Get3FeaturesCreators()
    {
        var creators = await _context.Creators
            .Select(c => new Creator
            {
                CreatorID = c.CreatorID,

                UserName = c.UserName,

                VIP = c.VIP,

                Email = c.Email,

                Phone = c.Phone

            })
            .ToListAsync();

        return creators;
    }


    [HttpGet("ProfilePicture/{CreatorID}")]
    public async Task<ActionResult<Creator>> GetProfilePictureByCreatorID(int CreatorID)
    {
        var creator = await _context.Creators
            .Where(c => c.CreatorID == CreatorID)
            .Select(c => new Creator
            {
                CreatorID = c.CreatorID,
                ProfilePicture = c.ProfilePicture
            })
            .FirstOrDefaultAsync();

        if (creator == null)
        {
            return NotFound();
        }

        return creator;
    }


    [HttpGet("NotProfile/NotBackground")]
    public async Task<ActionResult<IEnumerable<Creator>>> GetCreatorsNotProAndBack()
    {
        var creators = await _context.Creators
            .Select(c => new Creator
            {
                CreatorID = c.CreatorID,
                AccountID = c.AccountID,
                PaymentID = c.PaymentID,
                UserName = c.UserName,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Address = c.Address,
                Phone = c.Phone,
                LastLogDate = c.LastLogDate,
                AllowCommission = c.AllowCommission,
                Biography = c.Biography,
                VIP = c.VIP,
                FollowCounts = c.FollowCounts,

            })
            .ToListAsync();

        return creators;
    }
    [HttpGet("NotProfile/NotBackground/{CreatorID}")]
    public async Task<ActionResult<IEnumerable<Creator>>> GetCreatorsNotProAndBack(int CreatorID)
    {
        var creator = await _context.Creators
            .Where(c => c.CreatorID == CreatorID)
            .Select(c => new Creator
            {
                CreatorID = c.CreatorID,
                AccountID = c.AccountID,
                PaymentID = c.PaymentID,
                UserName = c.UserName,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Address = c.Address,
                Phone = c.Phone,
                LastLogDate = c.LastLogDate,
                AllowCommission = c.AllowCommission,
                Biography = c.Biography,
                VIP = c.VIP,
                FollowCounts = c.FollowCounts,

            })
            .ToListAsync();

        if (creator == null)
        {
            return NotFound();
        }

        return creator;
    }
}