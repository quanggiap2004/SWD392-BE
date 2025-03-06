using Common.Exceptions;
using Data.Repository.Interfaces;
using Domain.Domain.Context;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Implementations
{
    public class OnlineSerieBoxRepository : IOnlineSerieBoxRepository
    {
        private readonly BlindBoxSystemDbContext _context;

        public OnlineSerieBoxRepository(BlindBoxSystemDbContext context)
        {
            _context = context;
        }

        public async Task<OnlineSerieBox> AddOnlineSerieBoxAsync(OnlineSerieBox onlineSerieBox)
        {
            _context.OnlineSerieBoxes.Add(onlineSerieBox);
            await _context.SaveChangesAsync();
            return onlineSerieBox;
        }

        public async Task<OnlineSerieBox?> GetOnlineSerieBoxByIdAsync(int id)
        {
            return await _context.OnlineSerieBoxes.FindAsync(id);
        }

        public async Task<OnlineSerieBox> UpdateOnlineSerieBoxAsync(OnlineSerieBox onlineSerieBox)
        {
            var onlineSerieBoxToUpdate =await  _context.OnlineSerieBoxes.FindAsync(onlineSerieBox.OnlineSerieBoxId);
            if (onlineSerieBoxToUpdate == null)
            {
                throw new CustomExceptions.NotFoundException($"OnlineSerieBox with ID {onlineSerieBox.OnlineSerieBoxId} not found.");
            }
            onlineSerieBoxToUpdate.IsSecretOpen = onlineSerieBox.IsSecretOpen;
            onlineSerieBoxToUpdate.MaxTurn = onlineSerieBox.MaxTurn;
            onlineSerieBoxToUpdate.Name = onlineSerieBox.Name;
            onlineSerieBoxToUpdate.PriceAfterSecret = onlineSerieBox.PriceAfterSecret;
            onlineSerieBoxToUpdate.PriceIncreasePercent = onlineSerieBox.PriceIncreasePercent;
            await _context.SaveChangesAsync();
            return onlineSerieBox;
        }
    }
}
