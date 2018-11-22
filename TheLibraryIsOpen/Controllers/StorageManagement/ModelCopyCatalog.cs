﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLibraryIsOpen.db; // TODO: delete this when db code is removed
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Models.DBModels;
using static TheLibraryIsOpen.Constants.TypeConstants;

namespace TheLibraryIsOpen.Controllers.StorageManagement
{
    public class ModelCopyCatalog
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IdentityMap _im;

        public ModelCopyCatalog(UnitOfWork unitOfWork, IdentityMap im)
        {
            _unitOfWork = unitOfWork;
            _im = im;
        }

        //Create ModelCopy
        public Task<IdentityResult> CreateAsync(ModelCopy modelCopy)
        {
            if (modelCopy != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    // TODO: manage error if register return false
                    _unitOfWork.RegisterNew(modelCopy);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "modelCopy was null" });
            });
        }

        public Task<IdentityResult> UpdateAsync(ModelCopy modelCopy)
        {
            if (modelCopy != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _unitOfWork.RegisterDirty(modelCopy);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "modelCopy was null" });
            });
        }

        //Delete ModelCopy
        public Task<IdentityResult> DeleteAsync(ModelCopy modelCopy)
        {
            if (modelCopy != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    _unitOfWork.RegisterDeleted(modelCopy);
                    return IdentityResult.Success;
                });
            }
            return Task.Factory.StartNew(() =>
            {
                return IdentityResult.Failed(new IdentityError { Description = "modelCopy was null" });
            });
        }

        public async Task<ModelCopy> FindByIdAsync(int modelCopyId)
        {
            ModelCopy modelCopy = await _im.FindModelCopy((modelCopyId));

            return modelCopy;
        }

       




    }
}
