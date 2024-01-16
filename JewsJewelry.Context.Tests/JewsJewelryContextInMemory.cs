using JewsJewelry.Common.Entity.DBInterface;
using JewsJewelry.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Context.Tests
{
    public abstract class JewsJewelryContextInMemory : IAsyncDisposable
    {
        private readonly CancellationTokenSource _tokenSource;
        protected readonly CancellationToken cancellationToken;

        protected JewelryContext Context { get; }

        protected IUnitOfWork UnitOfWork => Context;

        protected IDBRead Reader => Context;

        protected IDBWriterContext WriterContext => new TestWriterContext(Context, UnitOfWork);

        protected JewsJewelryContextInMemory()
        {
            _tokenSource = new CancellationTokenSource();
            cancellationToken = _tokenSource.Token;
            var optionsBuilder = new DbContextOptionsBuilder<JewelryContext>()
                .UseInMemoryDatabase($"MoneronTests{Guid.NewGuid()}")
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            Context = new JewelryContext(optionsBuilder.Options);
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            try
            {
                Context.Database.EnsureDeletedAsync().Wait();
                Context.Dispose();
            }
            catch (ObjectDisposedException ex)
            {
                Trace.TraceError(ex.Message);
            }
        }

        async public ValueTask DisposeAsync()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            try
            {
                await Context.Database.EnsureDeletedAsync();
                await Context.DisposeAsync();
            }
            catch (ObjectDisposedException ex)
            {
                Trace.TraceError(ex.Message);
            }
        }
    }
}
