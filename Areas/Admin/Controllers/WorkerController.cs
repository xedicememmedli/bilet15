using Bilet_15.Areas.Admin.ViewModels.Worker;
using Bilet_15.DAL;
using Bilet_15.Models;
using Bilet_15.Utilities.Enum;
using Bilet_15.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Bilet_15.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WorkerController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public WorkerController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Worker> workers = await _context.Workers.ToListAsync();
            return View(workers);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]


        public async Task<IActionResult> Create(CreateWorkerVm createWorkerVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!createWorkerVm.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "Select correct image format!");

                return View(createWorkerVm);
            }

            if (createWorkerVm.ImageFile.ValidateSize(FileSize.Kb, 2))
            {

                ModelState.AddModelError("ImageFile", "Size must be less than 100kb");

                return View(createWorkerVm);
            }

            Worker worker = new()
            {
               
                FullName = createWorkerVm.FullName,
                Desicnation = createWorkerVm.Desicnation,
                Image = await createWorkerVm.ImageFile.CreateFileAsync(_env.WebRootPath, "assets", "images"),

            };
            await _context.Workers.AddAsync(worker); 
             

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            Worker? worker = await _context.Workers.FirstOrDefaultAsync(w => w.Id == id);
            if (worker == null)
            {
                return NotFound();
            }
            UpdateWorkerVm updateWorkerVm = new()
            {
                Id = worker.Id,
                FullName = worker.FullName,
                Desicnation = worker.Desicnation,
            };
            return View(updateWorkerVm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateWorkerVm updateWorkerVm)
        {
            Worker? worker = await _context.Workers.FirstOrDefaultAsync(w => w.Id == id);
            if (worker == null)
            {

                return NotFound();
            }

            if (updateWorkerVm.ImageFile != null)
            {
                if (!updateWorkerVm.ImageFile.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("ImageFile", "Select correct image format!");

                    return View(updateWorkerVm);
                }

                if (!updateWorkerVm.ImageFile.ValidateSize(FileSize.Gb, 2))
                {

                    ModelState.AddModelError("ImageFile", "Size must be less than 100kb");

                    return View(updateWorkerVm);
                }
                worker.Image = await updateWorkerVm.ImageFile.CreateFileAsync(_env.WebRootPath, "assets", "images");
            }

            worker.FullName = updateWorkerVm.FullName;
            worker.Desicnation = updateWorkerVm.Desicnation;
            return View(updateWorkerVm);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            Worker? worker = await _context.Workers.FirstOrDefaultAsync(w => w.Id == id);
            if (worker == null)
            {

                return NotFound();
            }
            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        }
    }

