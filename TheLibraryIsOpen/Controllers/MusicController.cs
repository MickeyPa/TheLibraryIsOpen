﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLibraryIsOpen.Controllers.StorageManagement;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Controllers
{
    public class MusicController : Controller
    {
        private readonly MusicCatalog _mc;
        private readonly ClientStore _cs;

        public MusicController(MusicCatalog mc, ClientStore cs)
        {
            _mc = mc;
            _cs = cs;
        }

        public async Task<IActionResult> Index()
        {
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();
            List<Music> musicList = await _mc.GetAllMusicDataAsync();
            return View(musicList);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var music = await _mc.FindMusicByIdAsync(id);
            if (music == null)
            {
                return NotFound();
            }

            return View(music);
        }

        public async Task<IActionResult> Create()
        {
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Music music)
        {
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();
            if (ModelState.IsValid)
            {
                await _mc.CreateMusicAsync(music);
                await _mc.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(music);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();

            var music = await _mc.FindMusicByIdAsync(id);
            if (music == null)
            {
                return NotFound();
            }
            return View(music);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Music music)
        {
            if (int.Parse(id) != music.MusicId)
            {
                return NotFound();
            }
            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();

            if (ModelState.IsValid)
            {
                try
                {
                    await _mc.UpdateMusicAsync(music);
                    await _mc.CommitAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(music);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();

            var music = await _mc.FindMusicByIdAsync(id);

            if (music == null)
            {
                return NotFound();
            }
            await _mc.DeleteMusicAsync(music);
            await _mc.CommitAsync();

            return View(music);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            bool isAdmin = await _cs.IsItAdminAsync(User.Identity.Name);
            if (!isAdmin)
                return Unauthorized();

            var music = await _mc.FindMusicByIdAsync(id);
            await _mc.DeleteMusicAsync(music);
            await _mc.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicExists(string id)
        {
            return (_mc.FindMusicByIdAsync(id) != null);
        }

    }
}