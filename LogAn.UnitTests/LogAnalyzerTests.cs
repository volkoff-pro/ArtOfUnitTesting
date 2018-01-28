using System;
using LogAn;
using NUnit;
using NUnit.Framework;
using NUnit.Compatibility;

namespace LogAn.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        private LogAnalyzer m_analyzer = null;
        IExtensionManager fakeExtensionMgr;

        private LogAnalyzer MakeAnalyzer(IExtensionManager mgr)
        {
            return new LogAnalyzer(mgr);
        }

        [SetUp]
        public void SetUp()
        {
            fakeExtensionMgr = new FakeExtensionManager();
            m_analyzer = new LogAnalyzer(fakeExtensionMgr);
        }

        [Test]
        public void IsValidFileName_NameSupportedExtension_ReturnsTrue()
        {
            FakeExtensionManager myFakeManager = new FakeExtensionManager();
            myFakeManager.WillBeValid = true;

            LogAnalyzer log = new LogAnalyzer(myFakeManager);
            bool result = log.IsValidLogFileName("short.ext");
            Assert.True(result);
        }

        [Test]
        public void IsValidFileName_EmptyFileName_Throws()
        {
            LogAnalyzer la = MakeAnalyzer(fakeExtensionMgr);
            var ex = Assert.Catch<Exception>(() => la.IsValidLogFileName(""));
            StringAssert.Contains("filename has to be provided", ex.Message);
        }

        [Test]
        public void IsValidFileName_BadExtension_ReturnFalse()
        {
            bool result = m_analyzer.IsValidLogFileName("fdasdffdsaadgg.foo");
            Assert.False(result);
        }

        [Test]
        public void IsValidFileName_GoodExtensionUppercase_ReturnsTrue()
        {
            bool result = m_analyzer.IsValidLogFileName("filewithgoodextension.SLF");
            Assert.True(result);
        }

        [TestCase("filewithgoodextension.slf")]
        [TestCase("filewithgoodextension.SLF")]
        public void IsValidFileName_ValidExtension_ReturnsTrue(string file)
        {
            bool result = m_analyzer.IsValidLogFileName(file);
            Assert.True(result);
        }

        [TestCase("filewithgoodextension.slf", true)]
        [TestCase("filewithgoodextension.SLF", true)]
        [TestCase("filewithbadextension.foo", false)]
        public void IsValidLogFileName_VariousExtensions_ChecksThem(string file, bool expected)
        {
            bool result = m_analyzer.IsValidLogFileName(file);
            Assert.AreEqual(expected, result);
        }

        [TearDown]
        public void TearDown()
        {
            m_analyzer = null;
        }
    }
}
