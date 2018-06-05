// <copyright file="DashboardService.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>


namespace Unity.Auth.Server.Controllers.Home
{
    using System.Linq;
    using System.Threading.Tasks;
    using Unity.Auth.Server.Controllers.Home.Models;
    using Unity.Auth.Server.Repositories;

    public class DashboardService
    {
        private readonly IUserRepository _userRepository;
        private readonly IClientRepository _clientRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="clientRepositor">The client repository.</param>
        public DashboardService(IUserRepository userRepository, IClientRepository clientRepository)
        {
            this._userRepository = userRepository;
            this._clientRepository = clientRepository;
        }

        /// <summary>
        /// Prepares the dashboard view.
        /// </summary>
        /// <returns></returns>
        public async Task<DashboardModel> PrepareDashboardView()
        {
            DashboardModel dashboard = new DashboardModel();
            var activeSessions = await this._userRepository.GetActiveSessions().ConfigureAwait(false);
            var registeredUsers = await this._userRepository.GetFindRegisteredUsers().ConfigureAwait(false);
            var registeredClients = await this._clientRepository.GetRegisteredClients().ConfigureAwait(false);
            var usage = await this._clientRepository.GetServerUsage().ConfigureAwait(false);

            dashboard.DashboardHeader = new DashboardHeaderModel
            {
                ActiveUsers = activeSessions,
                TotalApplications = registeredClients,
                TotalUsers = registeredUsers
            };

            dashboard.DashboardChart = usage.Select(s => new DashboardChartModel
            {
                Client = s.ClientId,
                ConnectedTime = s.ConnectedTime

            }).ToList();

            return dashboard;
        }
    }
}
