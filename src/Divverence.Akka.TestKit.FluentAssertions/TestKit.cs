//-----------------------------------------------------------------------
// <copyright file="TestKit.cs" company="Akka.NET Project">
//     Copyright (C) 2009-2018 Lightbend Inc. <http://www.lightbend.com>
//     Copyright (C) 2013-2018 .NET Foundation <https://github.com/akkadotnet/akka.net>
// </copyright>
//-----------------------------------------------------------------------

using System;
using Akka.Actor;
using Akka.Configuration;
using Akka.TestKit;

namespace Divverence.Akka.TestKit.FluentAssertions
{
    /// <summary>
    /// This class represents an Akka.NET TestKit that uses <a href="https://fluentassertions.com/">xUnit</a>
    /// as its testing framework.
    /// </summary>
    public class TestKit : TestKitBase , IDisposable
    {
        private bool _isDisposed; //Automatically initialized to false;

        /// <summary>
        /// <para>
        /// Initializes a new instance of the <see cref="TestKit"/> class.
        /// </para>
        /// <para>
        /// If no <paramref name="system"/> is passed in, a new system with
        /// <see cref="DefaultConfig"/> will be created.
        /// </para>
        /// </summary>
        /// <param name="system">The actor system to use for testing. The default value is <see langword="null"/>.</param>
        public TestKit(ActorSystem system = null)
            : base(Assertions, system)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestKit"/> class.
        /// </summary>
        /// <param name="config">The configuration to use for the system.</param>
        /// <param name="actorSystemName">The name of the system. The default name is "test".</param>
        public TestKit(Config config, string actorSystemName = null)
            : base(Assertions, config, actorSystemName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestKit"/> class.
        /// </summary>
        /// <param name="config">The configuration to use for the system.</param>
        public TestKit(string config)
            : base(Assertions, ConfigurationFactory.ParseString(config))
        {
        }

        /// <summary>
        /// A configuration that has just the default log settings enabled. The default settings can be found in
        /// <a href="https://github.com/akkadotnet/akka.net/blob/master/src/core/Akka.TestKit/Internal/Reference.conf">Akka.TestKit.Internal.Reference.conf</a>.
        /// </summary>
        public new static Config DefaultConfig => TestKitBase.DefaultConfig;

        /// <summary>
        /// A configuration that has all log settings enabled
        /// </summary>
        public new static Config FullDebugConfig => TestKitBase.FullDebugConfig;

        /// <summary>
        /// Commonly used assertions used throughout the testkit.
        /// </summary>
        protected static FluentAssertionsAssertions Assertions { get; } = new FluentAssertionsAssertions();

        /// <summary>
        /// This method is called when a test ends.
        /// 
        /// <remarks>
        /// If you override this, then make sure you either call base.AfterTest() or
        /// <see cref="TestKitBase.Shutdown(System.Nullable{System.TimeSpan},bool)">TestKitBase.Shutdown</see>
        /// to shut down the system. Otherwise a memory leak will occur.
        /// </remarks>
        /// </summary>
        protected virtual void AfterAll()
        {
            Shutdown();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            //Take this object off the finalization queue and prevent finalization code for this object
            //from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// if set to <c>true</c> the method has been called directly or indirectly by a  user's code.
        /// Managed and unmanaged resources will be disposed.<br /> if set to <c>false</c> the method
        /// has been called by the runtime from inside the finalizer and only unmanaged resources can
        ///  be disposed.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // If disposing equals false, the method has been called by the
            // runtime from inside the finalizer and you should not reference
            // other objects. Only unmanaged resources can be disposed.

            //Make sure Dispose does not get called more than once, by checking the disposed field
            if (!_isDisposed && disposing)
            {
                AfterAll();
            }

            _isDisposed = true;
        }
    }
}
