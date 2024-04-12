using DAL;
using Microsoft.AspNetCore.Mvc;

namespace News.Controllers
{
    public class NewsController : Controller
    {
            private readonly NewsDal newsDal;

            //Constractor
            public NewsController(NewsDal _newsDAL)
            {
                newsDal = _newsDAL;
            }

        // Function that return data by call to server side.
        [HttpGet]
            public async Task<IActionResult> GetNews()
            {
            try
                {
                var news = await newsDal.GetNews();
                    return Ok(news);
                }
                catch (Exception)
                {
                    return StatusCode(500, "Server Error");
                }
            }

            /// Function that return Item according to title.
            [HttpGet]
            public async Task<IActionResult> GetItem(string title)
            {
                try
                {
                    var newsItem = await newsDal.GetItem(title);
                    if (newsItem != null)
                    {
                        return Ok(newsItem);
                    }
                    else
                    {
                        return NotFound("the item not found");
                    }
                }
                catch (Exception)
                {
                    return StatusCode(500, "Server Error");
                }
                return BadRequest();
            }
        }
    }
