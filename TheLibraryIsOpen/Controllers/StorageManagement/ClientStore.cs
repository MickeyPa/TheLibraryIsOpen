﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheLibraryIsOpen.Database;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class ClientStore : IUserStore<Client>
    {
        private readonly Db _db;

        public ClientStore(Db db)
        {
            _db = db;
        }

        public Task<IdentityResult> CreateAsync(Client user, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    if (_db.GetClientByEmail(user.EmailAddress) != null)
                        return IdentityResult.Failed(new IdentityError { Description = "email already exists" });
                    _db.CreateClient(user);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "user was null" });
            });
        }

        public Task<IdentityResult> DeleteAsync(Client user, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.DeleteClient(user);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "user was null" });
            });
        }

        public void Dispose()
        { }

        public Task<Client> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetClientById(int.Parse(userId));
            });
            throw new ArgumentNullException("userId");
        }

        //input is email
        public Task<Client> FindByNameAsync(string email, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return Task.Factory.StartNew(() =>
                {
                    return _db.GetClientByEmail(email);
                });
            }
            throw new ArgumentNullException("userName");
        }

        public Task<string> GetNormalizedUserNameAsync(Client user, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetClientById(int.Parse(user.Id))?.EmailAddress?.Normalize() ?? null;
            });
            throw new ArgumentNullException("user");
        }

        public Task<string> GetUserIdAsync(Client user, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetClientByEmail(user.EmailAddress)?.Id ?? null;
            });
            throw new ArgumentNullException("user");
        }

        public Task<string> GetUserNameAsync(Client user, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetClientById(int.Parse(user.Id))?.EmailAddress ?? null;
            });
            throw new ArgumentNullException("user");
        }

        public Task SetNormalizedUserNameAsync(Client user, string normalizedName, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    user.EmailAddress = normalizedName;
                    _db.UpdateClient(user);
                });
            }
            throw new ArgumentNullException("user");
        }

        public Task SetUserNameAsync(Client user, string userName, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    user.EmailAddress = userName;
                    _db.UpdateClient(user);
                });
            }
            throw new ArgumentNullException("user");
        }

        public Task SetIsAdminAsync(Client user, Boolean isAdmin, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    user.IsAdmin = isAdmin;
                    _db.UpdateClient(user);
                });
            }
            throw new ArgumentNullException("user");
        }

        public Task<IdentityResult> UpdateAsync(Client user, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.UpdateClient(user);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "user was null" });
            });
        }

        public Task<List<Client>> GetAllClientsDataAsync() 
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetAllClients();
            });
        }

        public Task<bool> IsItAdminAsync(string clientEmail)
        {
            return Task.Factory.StartNew(() =>
            {
                Client client = _db.GetClientByEmail(clientEmail);
                if (client.IsAdmin == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }

        /*
         * The following methods are for the Music table
         */

        public Task<IdentityResult> CreateMusicAsync(Music music)
        {
            if (music != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.CreateMusic(music);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "Music object was null" });
            });
        }

        public Task UdpateMusicAsync(Music music)
        {
            if (music != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.UpdateMusic(music);
                });
            }
            throw new ArgumentNullException("music");
        }

        public Task<IdentityResult> DeleteMusicAsync(Music music)
        {
            if (music != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _db.DeleteMusic(music);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "Music object was null" });
            });
        }

        public Task<List<Music>> GetAllMusicsDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetAllMusics();
            });
        }

        public Task<Music> GetMusicByIdAsync(int musicId)
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetMusicById(musicId);
            });
            throw new ArgumentNullException("musicId");
        }

        public Task<Music> GetMusicByAsinAsync(string asin)
        {
            return Task.Factory.StartNew(() =>
            {
                return _db.GetMusicByAsin(asin);
            });
            throw new ArgumentNullException("asin");
        }
    }
}
