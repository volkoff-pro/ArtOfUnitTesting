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

        private LogAnalyzer MakeAnalyzer()
        {
            return new LogAnalyzer();
        }

        [SetUp]
        public void SetUp()
        {
            m_analyzer = new LogAnalyzer();
        }

        [Test]
        public void IsValidFileName_WhenCalled_ChangesWasLastFileNameValid()
        {
            LogAnalyzer la = MakeAnalyzer();
            la.IsValidLogFileName("badname.foo");
            Assert.False(la.WasLastFileNameValid);
        }

        [TestCase("badfile.foo", false)]
        [TestCase("goodfile.slf", true)]
        public void IsValidFileName_WhenCalled_ChangesWasLastFileNameValid(string file, bool expected)
        {
            LogAnalyzer la = MakeAnalyzer();
            la.IsValidLogFileName(file);
            Assert.AreEqual(expected, la.WasLastFileNameValid);
        }

        [Test]
        public void IsValidFileName_EmptyFileName_Throws()
        {
            LogAnalyzer la = MakeAnalyzer();
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
