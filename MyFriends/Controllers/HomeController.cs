using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFriends.DAL;
using MyFriends.Models;
using System.Diagnostics;

namespace MyFriends.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Friends()
        {
            List<Friend> Friends = Data.Get.Friends.ToList();
            return View(Friends);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View(new Friend());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddFriend(Friend friend)
        {
            Data.Get.Friends.Add(friend);
            Data.Get.SaveChanges();
            return RedirectToAction("Friends");
        }





        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Friends");
            }
            Friend? friend = Data.Get.Friends.FirstOrDefault(friend => friend.Id == id);
            if (friend == null)
            {
                return RedirectToAction("Friends");
            }

            return View(friend);
        }



        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult UpdateFriend(Friend newFriend)
        {
            if (newFriend == null)
            {
                return RedirectToAction("Friends");
            }

            Friend? existingFriend = Data.Get.Friends.FirstOrDefault(f => f.Id == newFriend.Id);
            if (existingFriend == null)
            {
                return RedirectToAction("Friends");
            }
            Data.Get.Entry(existingFriend).CurrentValues.SetValues(newFriend);
            Data.Get.SaveChanges();
            return RedirectToAction("Friends");
        }



        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Friend? friend = Data.Get.Friends.FirstOrDefault(friend => friend.Id == id);
            if (friend == null)
            {
                return NotFound();
            }
            Data.Get.Friends.Remove(friend);
            Data.Get.SaveChanges();


            return RedirectToAction(nameof(Friends));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Friends");
            }
            Friend? friend = Data.Get.Friends.Include(f => f.images).FirstOrDefault(friend => friend.Id == id);
            if (friend == null)
            {
                return RedirectToAction("Friends");
            }


            return View(friend);
        }



        public IActionResult AddNewImage(Friend friend)
        {
            Friend ? f = Data.Get.Friends.FirstOrDefault(f => f.Id == friend.Id);
            if (friend == null)
            {
                return NotFound();
            }
                     byte[] ? firstImage = friend.images.First().MyImage;
                if (firstImage == null)
                {
                    return NotFound();
                }
            f.AddImage(firstImage);
            Data.Get.SaveChanges();
            return RedirectToAction("Details", new { id = f.Id });
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
