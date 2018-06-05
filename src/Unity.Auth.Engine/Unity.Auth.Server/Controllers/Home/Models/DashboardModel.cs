// <copyright file="DashboardModel.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>

namespace Unity.Auth.Server.Controllers.Home.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class DashboardModel
    {
        /// <summary>
        /// Gets or sets the dashboard header.
        /// </summary>
        /// <value>
        /// The dashboard header.
        /// </value>
        public DashboardHeaderModel DashboardHeader { get; set; }

        /// <summary>
        /// Gets or sets the dashboard chart.
        /// </summary>
        /// <value>
        /// The dashboard chart.
        /// </value>
        public List<DashboardChartModel> DashboardChart { get; set; }

        /// <summary>
        /// Gets or sets the dashboard failure report.
        /// </summary>
        /// <value>
        /// The dashboard failure report.
        /// </value>
        public List<DashboardFailureReportModel> DashboardFailureReport { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DashboardHeaderModel
    {
        /// <summary>
        /// Gets or sets the total users.
        /// </summary>
        /// <value>
        /// The total users.
        /// </value>
        public int TotalUsers { get; set; }

        /// <summary>
        /// Gets or sets the total applications.
        /// </summary>
        /// <value>
        /// The total applications.
        /// </value>
        public int TotalApplications { get; set; }

        /// <summary>
        /// Gets or sets the active users.
        /// </summary>
        /// <value>
        /// The active users.
        /// </value>
        public int ActiveUsers { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DashboardChartModel
    {
        /// <summary>
        /// Gets or sets the usage time.
        /// </summary>
        /// <value>
        /// The usage time.
        /// </value>
        public DateTime ConnectedTime { get; set; }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public string Client { get; set; }

        /// <summary>
        /// Gets or sets the usage.
        /// </summary>
        /// <value>
        /// The usage.
        /// </value>
        public int Usage { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DashboardFailureReportModel
    {
        /// <summary>
        /// Gets or sets the date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public string Client { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the type of the failed.
        /// </summary>
        /// <value>
        /// The type of the failed.
        /// </value>
        public FailedType FailedType { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum FailedType
    {
        /// <summary>
        /// The invalid attempt locked.
        /// </summary>
        InvalidAttemptLocked,

        /// <summary>
        /// The application failure.
        /// </summary>
        ApplicationFailure
    }
}
