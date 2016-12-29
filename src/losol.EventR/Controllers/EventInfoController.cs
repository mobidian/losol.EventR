using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using losol.EventR.Data;
using losol.EventR.Models;
using Microsoft.AspNetCore.Authorization;

namespace losol.EventR.Controllers
{
    [Authorize]
    public class EventInfoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventInfoController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: EventInfo
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.EventInfo.ToListAsync());
        }

        [AllowAnonymous]
        // GET: EventInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventInfo = await _context.EventInfo
                .SingleOrDefaultAsync(m => m.EventInfoId == id);
            if (id == null)
            {
                return NotFound();
            }

            return View(eventInfo);
        }

        // GET: EventInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EventInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,Name,Description,Publish,Location,StartDate,StartTime,EndDate,EndTime,LastEnrolmentDate,LastWithdrawalDate,MaxAttendees,Price,VatPercent,MoreInformation,WelcomeLetter,DiplomaDescription")] EventInfo eventInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(eventInfo);
        }

        // GET: EventInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventInfo = await _context.EventInfo.SingleOrDefaultAsync(m => m.EventInfoId == id);
            if (eventInfo == null)
            {
                return NotFound();
            }
            return View(eventInfo);
        }

        // POST: EventInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventInfoId,Name,Description,Publish,Location,StartDate,StartTime,EndDate,EndTime,LastEnrolmentDate,LastWithdrawalDate,MaxAttendees,Price,VatPercent,MoreInformation,WelcomeLetter,DiplomaDescription")] EventInfo eventInfo)
        {
            if (id != eventInfo.EventInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventInfoExists(eventInfo.EventInfoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(eventInfo);
        }

        // GET: EventInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventInfo = await _context.EventInfo
                .SingleOrDefaultAsync(m => m.EventInfoId == id);
            if (eventInfo == null)
            {
                return NotFound();
            }

            return View(eventInfo);
        }

        // POST: EventInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventInfo = await _context.EventInfo.SingleOrDefaultAsync(m => m.EventInfoId == id);
            _context.EventInfo.Remove(eventInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EventInfoExists(int eventInfoId)
        {
            return _context.EventInfo.Any(e => e.EventInfoId == eventInfoId);
        }
    }
}
