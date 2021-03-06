﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebAPI.OutputCache.Cache;

namespace WebAPI.OutputCache.Tests
{
    [TestFixture]
    public class MemoryCacheDefaultTests
    {
        [Test]
        public void remove_startswith_cascades_to_all_dependencies()
        {
            IApiOutputCache cache = new MemoryCacheDefault();
            cache.Add("base", "abc", DateTime.Now.AddSeconds(60));
            cache.Add("key1","abc", DateTime.Now.AddSeconds(60), "base");
            cache.Add("key2", "abc", DateTime.Now.AddSeconds(60), "base");
            cache.Add("key3", "abc", DateTime.Now.AddSeconds(60), "base");
            Assert.IsNotNull(cache.Get("key1"));
            Assert.IsNotNull(cache.Get("key2"));
            Assert.IsNotNull(cache.Get("key3"));

            cache.RemoveStartsWith("base");

            Assert.IsNull(cache.Get("base"));
            Assert.IsNull(cache.Get("key1"));
            Assert.IsNull(cache.Get("key2"));
            Assert.IsNull(cache.Get("key3"));
        }
    }
}
