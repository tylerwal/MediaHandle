﻿using Microsoft.Win32.SafeHandles;
using System;
using SystemInterface.Microsoft.Win32.SafeHandles;

namespace SystemWrapper.Microsoft.Win32.SafeHandles
{
    /// <summary>
    /// SafeRegistryHandleWrap should be used instead of SafeRegistryHandle
    /// and implements ISafeRegistryHandle
    /// </summary>
    public class SafeRegistryHandleWrap : ISafeRegistryHandle
    {
        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="safeRegistryHandle"></param>
        public SafeRegistryHandleWrap(SafeRegistryHandle safeRegistryHandle)
        {
            SafeRegistryHandleInstance = safeRegistryHandle;
        }

        #region Implementation of IDisposable
        /// <summary>
        /// The dispose method
        /// </summary>
        public void Dispose()
        {
            SafeRegistryHandleInstance.Dispose();
        }

        #endregion

        #region Implementation of ISafeHandle
        /// <summary>
        /// 
        /// </summary>
        public bool IsClosed
        {
            get { return SafeRegistryHandleInstance.IsClosed; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsInvalid
        {
            get { return SafeRegistryHandleInstance.IsInvalid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            SafeRegistryHandleInstance.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        public void DangerousAddRef(ref bool success)
        {
            SafeRegistryHandleInstance.DangerousAddRef(ref success);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IntPtr DangerousGetHandle()
        {
            return SafeRegistryHandleInstance.DangerousGetHandle();
        }
        /// <summary>
        /// 
        /// </summary>
        public void DangerousRelease()
        {
            SafeRegistryHandleInstance.DangerousRelease();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetHandleAsInvalid()
        {
            SafeRegistryHandleInstance.SetHandleAsInvalid();
        }

        #endregion

        #region Implementation of ISafeRegistryHandle
        /// <summary>
        /// 
        /// </summary>
        public SafeRegistryHandle SafeRegistryHandleInstance { get; private set; }

        #endregion
    }
}
