using Helper;
using Model;
using Newtonsoft.Json;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class ImageService:IImageService
    {
        public List<ImageModel> GetAllImage()
        {
            var body = "";
            List<ImageModel> ImageModels = ServerResponse.Invoke<List<ImageModel>>("api/image/getDetails", body, "get");
            return ImageModels;
        }
        public ImageModel Create(ImageModel imageModel)
        {
            var body = JsonConvert.SerializeObject(imageModel);
            ImageModel ImageCreate = ServerResponse.Invoke<ImageModel>("api/image/create", body, "Post");
            return ImageCreate;
        }

       
    }
}
