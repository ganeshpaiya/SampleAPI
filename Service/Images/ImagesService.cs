using Data.Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Images
{
    public class ImagesService : IImagesService
    {
        private readonly ApiContext context;
        public ImagesService(ApiContext context)
        {
            this.context = context;
        }
        public async Task<bool> DeleteImage(int id)
        {
            var image = await context.Images.FindAsync(id);

            if (image == null)
            {
                return false;
            }

            context.Images.Remove(image);
            await context.SaveChangesAsync();

            return true;
        }
        public async Task<Image> GetImage(int id)
        {
            var image = await context.Images.FindAsync(id);
            return image;
        }

        public async Task<IEnumerable<Image>> GetproductImages(int productId)
        {
            return await context.Images.Where(c => c.ProductId == productId).AsNoTracking().ToListAsync();
        }

        public async Task<int> PostImage(Image image)
        {
            context.Images.Add(image);
            await context.SaveChangesAsync();

            return image.Id;
        }

        public async Task<bool> PutImage(int id, Image image)
        {
            context.Entry(image).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        private bool ImageExists(int id)
        {
            return context.Images.Any(e => e.Id == id);
        }
    }
}
