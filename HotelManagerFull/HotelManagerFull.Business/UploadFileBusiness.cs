using HotelManagerFull.Repository;
using HotelManagerFull.Share.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HotelManagerFull.Business
{
    /// <summary>
    /// IUploadFileBusiness
    /// </summary>
    public interface IUploadFileBusiness
    {
        /// <summary>
        /// UploadSingleFileAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<bool> UploadSingleFileAsync(IFormFile file, string type);

        /// <summary>
        /// UploadMultipleFileAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<bool> UploadMultipleFileAsync(List<IFormFile> listFile, string type);
    }

    /// <summary>
    /// UploadFileBusiness
    /// </summary>
    public class UploadFileBusiness : IUploadFileBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private static string _webRootPath = string.Empty;

        /// <summary>
        /// UploadFileBusiness
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="hostingEnvironment"></param>
        public UploadFileBusiness(IUnitOfWork unitOfWork, IHostingEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webRootPath = hostingEnvironment.WebRootPath;
        }

        /// <summary>
        /// UploadSingleFileAsync
        /// </summary>
        /// <param name="file"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<bool> UploadSingleFileAsync(IFormFile file, string type)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    if (file == null) return false;

                    var url = string.Empty;
                    switch (type)
                    {
                        case Constants.FileType.PROVINCES:
                            url = _webRootPath + Constants.UploadUrl.UPLOAD_PROVINCES;
                            break;
                        case Constants.FileType.HOTELS:
                            url = _webRootPath + Constants.UploadUrl.UPLOAD_HOTELS;
                            break;
                        case Constants.FileType.SERVICES:
                            url = _webRootPath + Constants.UploadUrl.UPLOAD_SERVICES;
                            break;
                        default:
                            break;
                    }

                    UploadFileToFolder(file, url);
                    _unitOfWork.CommitTransaction();
                    return true;
                }
                catch (Exception)
                {
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        /// <summary>
        /// UploadMultipleFileAsync
        /// </summary>
        /// <param name="listFile"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<bool> UploadMultipleFileAsync(List<IFormFile> listFile, string type)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    if (listFile == null || listFile.Count <= 0) return false;

                    var url = string.Empty;
                    switch (type)
                    {
                        case Constants.FileType.ROOMS:
                            url = _webRootPath + Constants.UploadUrl.UPLOAD_ROOMS;
                            break;
                        default:
                            break;
                    }

                    foreach (var item in listFile)
                    {
                        UploadFileToFolder(item, url);
                    }
                    _unitOfWork.CommitTransaction();
                    return true;
                }
                catch (Exception)
                {
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        /// <summary>
        /// UploadFileToFolder
        /// </summary>
        /// <param name="file"></param>
        /// <param name="url"></param>
        private void UploadFileToFolder(IFormFile file, string url)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    // Path Folder
                    if (!Directory.Exists(url))
                    {
                        Directory.CreateDirectory(url);
                    }

                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), url);
                    var fullPath = Path.Combine(pathToSave, file.FileName);

                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
