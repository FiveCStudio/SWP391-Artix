﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using System.Net;
[ApiController]
[Route("api/artworks")]
public class ArtworksController : ControllerBase
{
    private readonly ApplicationDbContext _context; // Replace YourDbContext with your actual database context

    public ArtworksController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/artworks

    [HttpGet]
    public async Task<IActionResult> GetArtworks()
    {
        var artworks = await _context.Artworks
            .OrderBy(a => a.DateCreated) // Sắp xếp theo ngày tạo
            .Take(5) // Lấy 5 artwork đầu tiên
            .Include(a => a.ArtworkTag) // Kèm theo thông tin tag của artwork
            .Select(a => new Artworks // Tạo đối tượng DTO để chứa thông tin cần thiết
            {
                ArtworkID = a.ArtworkID,
                CreatorID = a.CreatorID,
                ArtworkName = a.ArtworkName,
                Description = a.Description,
                DateCreated = a.DateCreated,
                Likes = a.Likes,
                Purchasable = a.Purchasable,
                Price = a.Price,
                ImageFile = a.ImageFile,
                ArtworkTag = a.ArtworkTag
            })
            .ToListAsync();

        if (artworks == null || artworks.Count == 0)
        {
            return NotFound();
        }

        return Ok(artworks);
    }
    [HttpGet("ArtworkNotImageFile/{ArtworkID}")]
    public async Task<IActionResult> GetArtwork(int ArtworkID)
    {
        var artwork = await _context.Artworks
            .Where(a => a.ArtworkID == ArtworkID)
            .Select(a => new Artworks
            {
                ArtworkID = a.ArtworkID,
                CreatorID = a.CreatorID,
                ArtworkName = a.ArtworkName,
                Description = a.Description,
                DateCreated = a.DateCreated,
                Likes = a.Likes,
                Purchasable = a.Purchasable,
                Price = a.Price,
                ArtworkTag = a.ArtworkTag
            })
            .FirstOrDefaultAsync();

        if (artwork == null)
        {
            return NotFound();
        }

        return Ok(artwork);
    }

    [HttpGet("NotImage")]
    public async Task<IActionResult> GetArtworksNotImage()
    {
        var artworks = await _context.Artworks
            .OrderBy(a => a.DateCreated) // Sắp xếp theo ngày tạo
            .Take(5) // Lấy 5 artwork đầu tiên
            .Include(a => a.ArtworkTag) // Kèm theo thông tin tag của artwork
            .Select(a => new Artworks // Tạo đối tượng DTO để chứa thông tin cần thiết
            {
                ArtworkID = a.ArtworkID,
                CreatorID = a.CreatorID,
                ArtworkName = a.ArtworkName,
                Description = a.Description,
                DateCreated = a.DateCreated,
                Likes = a.Likes,
                Purchasable = a.Purchasable,
                Price = a.Price,
                ArtworkTag = a.ArtworkTag
            })
            .ToListAsync();

        if (artworks == null || artworks.Count == 0)
        {
            return NotFound();
        }

        return Ok(artworks);
    }



    [HttpGet("recent7artworks")]
    public async Task<IActionResult> GetRecent7Artworks()
    {
        var recentArtworks = await _context.Artworks
            .OrderByDescending(a => a.DateCreated) // Sắp xếp theo ngày tạo giảm dần (tức là ngày gần nhất đăng lên sẽ ở đầu)
            .Take(7) // Chỉ lấy 7 artwork đầu tiên
            .Select(a => new
            {
                ArtworkID = a.ArtworkID,
                CreatorID = a.CreatorID,
                ArtworkName = a.ArtworkName,
                Description = a.Description,
                DateCreated = a.DateCreated,
                Likes = a.Likes,
                Purchasable = a.Purchasable,
                Price = a.Price,
                ImageFile = a.ImageFile
            })
            .ToListAsync();

        if (recentArtworks == null || recentArtworks.Count == 0)
        {
            return NotFound();
        }

        return Ok(recentArtworks);
    }

    [HttpGet("recent7artworksNotImage")]
    public async Task<IActionResult> GetRecent7ArtworksNotImage()
    {
        var recentArtworks = await _context.Artworks
            .OrderByDescending(a => a.DateCreated) // Sắp xếp theo ngày tạo giảm dần (tức là ngày gần nhất đăng lên sẽ ở đầu)
            .Take(7) // Chỉ lấy 7 artwork đầu tiên
            .Select(a => new
            {
                ArtworkID = a.ArtworkID,
                CreatorID = a.CreatorID,
                ArtworkName = a.ArtworkName,
                Description = a.Description,
                DateCreated = a.DateCreated,
                Likes = a.Likes,
                Purchasable = a.Purchasable,
                Price = a.Price,
                
            })
            .ToListAsync();

        if (recentArtworks == null || recentArtworks.Count == 0)
        {
            return NotFound();
        }

        return Ok(recentArtworks);
    }

    [HttpGet("recent-artwork-count")]
    public async Task<IActionResult> GetRecentArtworkCount()
    {
        
        var currentDate = DateTime.UtcNow;

        // Lấy ngày 7 ngày trước
        var sevenDaysAgo = currentDate.AddDays(-7);

        // Đếm số artwork được đăng trong 7 ngày gần nhất
        var recentArtworkCount = await _context.Artworks
            .Where(a => a.DateCreated >= sevenDaysAgo && a.DateCreated <= currentDate)
            .CountAsync();

        return Ok(recentArtworkCount);
    }




    [HttpGet("{artworkId}/tags")]
    public async Task<IActionResult> GetArtworkTags(int artworkId)
    {
        var artworkTags = await _context.ArtworkTag
            .Where(at => at.ArtworkID == artworkId)
            .Join(_context.Tags,
                at => at.TagID,
                tag => tag.TagID,
                (at, tag) => new
                {
                    TagID = tag.TagID,
                    TagName = tag.TagName
                })
            .Distinct()
            .ToListAsync();

        if (artworkTags == null || artworkTags.Count == 0)
        {
            return NotFound();
        }

        return Ok(artworkTags);
    }




    [HttpGet("recent-artworks")]
    public async Task<ActionResult<IEnumerable<Artworks>>> GetRecentArtworks()
    {
        var recentArtworks = await _context.Artworks
            .OrderByDescending(a => a.ArtworkID) // Sử dụng ID nếu cần
            .Take(2)
            .Include(a => a.ArtworkTag) // Kèm theo thông tin tag của artwork
        .Select(a => new Artworks // Tạo đối tượng DTO để chứa thông tin cần thiết
        {
            ArtworkID = a.ArtworkID,
            CreatorID = a.CreatorID,
            ArtworkName = a.ArtworkName,
            Description = a.Description,
            DateCreated = a.DateCreated,
            Likes = a.Likes,
            Purchasable = a.Purchasable,
            Price = a.Price,
            ImageFile = a.ImageFile,
            ArtworkTag = a.ArtworkTag
        })
        .ToListAsync();

        return recentArtworks;
    }

    [HttpGet("ByCreatorID/{CreatorID}")]
    public async Task<IActionResult> GetArtworkByCreatorID(int CreatorID)
    {
        var artworks = await _context.Artworks
            .Include(a => a.ArtworkTag)
            .Where(a => a.CreatorID == CreatorID)
            .Select(a => new Artworks
            {
                ArtworkID = a.ArtworkID,
                CreatorID = a.CreatorID,
                ArtworkName = a.ArtworkName,
                Description = a.Description,
                DateCreated = a.DateCreated,
                Likes = a.Likes,
                Purchasable = a.Purchasable,
                Price = a.Price,
                ImageFile = a.ImageFile,
                ArtworkTag = a.ArtworkTag
            })
            .ToListAsync();

        if (artworks == null || artworks.Count == 0)
        {
            return NotFound();
        }

        return Ok(artworks);
    }



    [HttpGet("ByCreatorIDNotImage/{CreatorID}")]
    public async Task<IActionResult> GetArtworkByCreatorIDNotImage(int CreatorID)
    {
        var artworks = await _context.Artworks
            .Include(a => a.ArtworkTag)
            .Where(a => a.CreatorID == CreatorID)
            .Select(a => new Artworks
            {
                ArtworkID = a.ArtworkID,
                CreatorID = a.CreatorID,
                ArtworkName = a.ArtworkName,
                Description = a.Description,
                DateCreated = a.DateCreated,
                Likes = a.Likes,
                Purchasable = a.Purchasable,
                Price = a.Price,
                ArtworkTag = a.ArtworkTag
            })
            .ToListAsync();

        if (artworks == null || artworks.Count == 0)
        {
            return NotFound();
        }

        return Ok(artworks);
    }




    // GET: api/artworks/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetArtworkById(int id)
    {
        var artwork = await _context.Artworks
            .Include(a => a.ArtworkTag)
            .Select(a => new Artworks
            {
                ArtworkID = a.ArtworkID,
                CreatorID = a.CreatorID,
                ArtworkName = a.ArtworkName,
                Description = a.Description,
                DateCreated = a.DateCreated,
                Likes = a.Likes,
                Purchasable = a.Purchasable,
                Price = a.Price,
                ImageFile = a.ImageFile,
                ArtworkTag = a.ArtworkTag
            })
            .FirstOrDefaultAsync(a => a.ArtworkID == id);

        if (artwork == null)
        {
            return NotFound();
        }

        return Ok(artwork);
    }


    [HttpGet("total-likes/{CreatorId}")]
    public async Task<ActionResult<int>> GetTotalLikesByCreatorId(int CreatorId)
    {
        try
        {
            // Tìm tất cả các tác phẩm của một tác giả dựa trên CreatorID
            var artworks = await _context.Artworks.Where(a => a.CreatorID == CreatorId).ToListAsync();

            // Tính tổng lượng like của tất cả các tác phẩm
            int totalLikes = artworks.Sum(a => a.Likes);

            return totalLikes;
        }
        catch (Exception ex)
        {
            // Xử lý lỗi nếu có
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpGet("recent-artworks-no-image/{CreatorId}")]
    public async Task<ActionResult<IEnumerable<Artworks>>> GetRecentArtworksWithoutImageByCreatorId(int CreatorId)
    {
        try
        {
            // Lấy 4 tác phẩm gần nhất không có hình ảnh của một tác giả dựa trên CreatorID
            var recentArtworks = await _context.Artworks
                .Where(a => a.CreatorID == CreatorId && a.ImageFile == null)
                .OrderByDescending(a => a.DateCreated)
                .Take(4)
                .ToListAsync();

            return recentArtworks;
        }
        catch (Exception ex)
        {
            // Xử lý lỗi nếu có
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpGet("recent-artworks-with-image/{CreatorId}")]
    public async Task<ActionResult<IEnumerable<Artworks>>> GetRecentArtworksWithImageByCreatorId(int CreatorId)
    {
        try
        {
            // Lấy 4 tác phẩm gần nhất có hình ảnh của một tác giả dựa trên CreatorID
            var recentArtworksWithImage = await _context.Artworks
                .Where(a => a.CreatorID == CreatorId && a.ImageFile != null)
                .OrderByDescending(a => a.DateCreated)
                .Take(4)
                .ToListAsync();

            return recentArtworksWithImage;
        }
        catch (Exception ex)
        {
            // Xử lý lỗi nếu có
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
        }
    }



    // POST: api/artworks

    [HttpPost]
    public async Task<IActionResult> CreateArtwork([FromBody] Artworks artwork)
    {
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            // Kiểm tra xem CreatorID có tồn tại không
            if (!_context.Creators.Any(c => c.CreatorID == artwork.CreatorID))
            {
                return BadRequest("CreatorID không tồn tại");
            }

            // Kiểm tra xem TagID có tồn tại không
            if (artwork.ArtworkTag != null && artwork.ArtworkTag.Any())
            {
                var invalidTagIds = artwork.ArtworkTag
                    .Where(at => !_context.Tags.Any(t => t.TagID == at.TagID))
                    .Select(at => at.TagID)
                    .ToList();

                if (invalidTagIds.Any())
                {
                    return BadRequest($"TagID không tồn tại: {string.Join(", ", invalidTagIds)}");
                }
            }

            // Thêm artwork vào cơ sở dữ liệu
            _context.Artworks.Add(artwork);
            await _context.SaveChangesAsync();



            // Lưu trữ ArtworkID đã được tạo tự động
            var artworkId = artwork.ArtworkID;


            // Thêm ArtworkTag vào cơ sở dữ liệu
            foreach (var artworkTag in artwork.ArtworkTag)
            {
                // Thiết lập ArtworkID với giá trị đã được tạo tự động
                artworkTag.ArtworkID = artworkId;

                _context.ArtworkTag.Add(artworkTag);
            }

            await _context.SaveChangesAsync();
            scope.Complete();
            return Ok(artwork);
        }
    }



    // PUT: api/artworks/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutArtwork(int id, [FromBody] Artworks artworkRequest)
    {
        if (id != artworkRequest.ArtworkID)
        {
            return BadRequest("Invalid ID");
        }

        var existingArtwork = await _context.Artworks
            .Include(a => a.ArtworkTag)
            .FirstOrDefaultAsync(a => a.ArtworkID == id);

        if (existingArtwork == null)
        {
            return NotFound();
        }

        _context.Entry(existingArtwork).State = EntityState.Detached;

        existingArtwork.ArtworkName = artworkRequest.ArtworkName;
        existingArtwork.Description = artworkRequest.Description;
        existingArtwork.Likes = artworkRequest.Likes;
        existingArtwork.Purchasable = artworkRequest.Purchasable;
        existingArtwork.Price = artworkRequest.Price;

        // Update tags
        existingArtwork.ArtworkTag = artworkRequest.ArtworkTag.Select(tag => new ArtworkTag
        {
            TagID = tag.TagID
        }).ToList();

        _context.Entry(existingArtwork).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ArtworkExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok("Artwork updated successfully");
    }

    private bool ArtworkExists(int id)
    {
        return _context.Artworks.Any(e => e.ArtworkID == id);
    }











    //GET: API/artwork/{Top10Liked}
    [HttpGet("Top10Liked")]
    public async Task<ActionResult<IEnumerable<Artworks>>> GetTopLikedArtworks()
    {
        var topLikedArtworks = await _context.Artworks
            .OrderByDescending(a => a.Likes)
            .Take(10)
            .ToListAsync();

        if (topLikedArtworks == null || topLikedArtworks.Count == 0)
        {
            return NotFound();
        }

        return topLikedArtworks;
    }

    // GET: api/artworks/random11
    [HttpGet("random10")]
    public async Task<IActionResult> GetRandom11Artworks()
    {
        // Lấy danh sách tất cả các artworks từ cơ sở dữ liệu
        var allArtworks = await _context.Artworks.ToListAsync();

        // Kiểm tra xem có artworks nào không
        if (allArtworks.Count == 0)
        {
            return NotFound("Không có artworks nào trong cơ sở dữ liệu.");
        }

        // Lấy 11 artworks ngẫu nhiên từ danh sách tất cả các artworks
        var randomArtworks = GetRandomElements(allArtworks, 10);

        return Ok(randomArtworks);
    }

    // Hàm chọn ngẫu nhiên các phần tử từ danh sách
    private List<Artworks> GetRandomElements(List<Artworks> list, int count)
    {
        var random = new Random();
        var randomArtworks = new List<Artworks>();

        while (randomArtworks.Count < count)
        {
            var index = random.Next(0, list.Count);
            var artwork = list[index];

            // Kiểm tra xem artwork đã được chọn trước đó chưa
            if (!randomArtworks.Contains(artwork))
            {
                randomArtworks.Add(artwork);
            }
        }

        return randomArtworks;
    }


    [HttpGet("recent-likes-summary")]
    public async Task<IActionResult> GetRecentArtworkLikesSummary()
    {
        try
        {
            // Lấy ngày hiện tại
            var currentDate = DateTime.UtcNow;

            // Lấy ngày 7 ngày trước
            var sevenDaysAgo = currentDate.AddDays(-7);

            // Lấy danh sách ngày trong khoảng từ ngày 7 ngày trước đến ngày hiện tại
            var dateRange = Enumerable.Range(0, 7).Select(offset => currentDate.AddDays(-offset).Date).ToList();

            // Tạo danh sách để lưu trữ kết quả
            var likeSummary = new List<ArtworkLikesByDate>();

            // Duyệt qua từng ngày trong khoảng thời gian
            foreach (var date in dateRange)
            {

                // Lấy thông tin của các artworks được tạo vào ngày đó
                var artworks = await _context.Artworks
                    .Where(a => a.DateCreated.Date == date)
                    .ToListAsync();

                // Tính tổng lượng like của các artworks
                var likes = artworks.Sum(a => a.Likes);

                // Thêm thông tin của từng artwork vào danh sách kết quả
                foreach (var artwork in artworks)
                {
                    likeSummary.Add(new ArtworkLikesByDate
                    {
                        Date = date,
                        likes = likes,
                        ArtworkID = artwork.ArtworkID,
                        ArtworkName = artwork.ArtworkName
                    });
                }

            }

            return Ok(likeSummary);
        }
        catch (Exception ex)
        {
            // Xử lý lỗi nếu có
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
        }
    }

    public class ArtworkLikesByDate
    {
        public DateTime Date { get; set; }
        public int likes { get; set; }

        public int ArtworkID { get; set; }
        public string ArtworkName { get; set; }
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArtwork(int id)
    {
        var artwork = await _context.Artworks.FindAsync(id);
        if (artwork == null)
        {
            return NotFound();
        }

     
        // Xóa các bản ghi từ bảng ArtworkTag liên quan đến Artworks
        var relatedArtworkTags = _context.ArtworkTag.Where(at => at.ArtworkID == id);
        _context.ArtworkTag.RemoveRange(relatedArtworkTags);

        // Sau đó mới xóa bản ghi từ bảng Artworks
        _context.Artworks.Remove(artwork);

        await _context.SaveChangesAsync();

        return NoContent();
    }


}