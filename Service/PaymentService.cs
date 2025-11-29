using AutoMapper;
using Contracts.Logger;
using Contracts.IRepository;
using Services.Contracts;
using Shared.DataTransferObject.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager loggerManager;
        private readonly IMapper mapper;

        public PaymentService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.loggerManager = loggerManager;
            this.mapper = mapper;
        }

        public async Task HandlePaymobCallbackAsync(PaymobCallbackDto dto)
        {
            await repositoryManager.Payment.HandlePaymobCallbackAsync(dto);
        }
    }
}
