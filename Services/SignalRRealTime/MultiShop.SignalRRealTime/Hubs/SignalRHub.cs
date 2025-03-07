﻿using Microsoft.AspNetCore.SignalR;
using MultiShop.SignalRRealTime.Services.SignalRCommentServices;

namespace MultiShop.SignalRRealTime.Hubs
{
    public class SignalRHub : Hub
    {
        private readonly ISignalRCommentService _signalRCommentService;
        // private readonly ISignalRMessageService _signalRMessageService;
        public SignalRHub(ISignalRCommentService signalRCommentService)
        {
            _signalRCommentService = signalRCommentService;
        }

        public async Task SendStatisticCount()
        {
            var getTotalCommentCount = await _signalRCommentService.GetTotalCommentCount();
            await Clients.All.SendAsync("ReceiveCommentCount", getTotalCommentCount);

        }
    }
}
