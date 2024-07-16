﻿namespace dotnet60_example.ViewModels
{
    public class PermissionVM
    {
        public int PermissionId { get; set; }

        public string? ControllerName { get; set; }

        public string? ActionName { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}