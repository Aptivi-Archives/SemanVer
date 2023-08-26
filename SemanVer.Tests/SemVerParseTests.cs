/*
 * MIT License
 *
 * Copyright (c) 2022-2023 Aptivi
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 * 
 */

using SemanVer.Instance;

namespace SemanVer.Tests
{
    public class SemVerParseTests
    {
        [Test]
        [TestCase("1.0.0",                  ExpectedResult = new object[] { 1, 0, 0, "", "" })]
        [TestCase("1.0.0-alpha1",           ExpectedResult = new object[] { 1, 0, 0, "alpha1", "" })]
        [TestCase("1.0.0+234F234D",         ExpectedResult = new object[] { 1, 0, 0, "", "234F234D" })]
        [TestCase("1.0.0-alpha1+234F234D",  ExpectedResult = new object[] { 1, 0, 0, "alpha1", "234F234D" })]
        [TestCase("0.1.0",                  ExpectedResult = new object[] { 0, 1, 0, "", "" })]
        [TestCase("0.1.0-alpha1",           ExpectedResult = new object[] { 0, 1, 0, "alpha1", "" })]
        [TestCase("0.1.0+234F234D",         ExpectedResult = new object[] { 0, 1, 0, "", "234F234D" })]
        [TestCase("0.1.0-alpha1+234F234D",  ExpectedResult = new object[] { 0, 1, 0, "alpha1", "234F234D" })]
        [TestCase("0.0.1",                  ExpectedResult = new object[] { 0, 0, 1, "", "" })]
        [TestCase("0.0.1-alpha1",           ExpectedResult = new object[] { 0, 0, 1, "alpha1", "" })]
        [TestCase("0.0.1+234F234D",         ExpectedResult = new object[] { 0, 0, 1, "", "234F234D" })]
        [TestCase("0.0.1-alpha1+234F234D",  ExpectedResult = new object[] { 0, 0, 1, "alpha1", "234F234D" })]
        public object[] TestSemVer(string version)
        {
            SemVer semVer = SemVer.Parse(version);
            return new object[]
            { 
                semVer.MajorVersion,
                semVer.MinorVersion,
                semVer.PatchVersion,
                semVer.PreReleaseInfo,
                semVer.BuildMetadata
            };
        }

        [Test]
        [TestCase("1.0.0", "1.0.0",                                     ExpectedResult = true)]
        [TestCase("1.0.0-alpha1", "1.0.0",                              ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0-alpha1",                              ExpectedResult = false)]
        [TestCase("1.0.0-alpha1", "1.0.0-alpha1",                       ExpectedResult = true)]
        [TestCase("1.0.0+234F234D", "1.0.0",                            ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0+234F234D",                            ExpectedResult = false)]
        [TestCase("1.0.0+234F234D", "1.0.0+234F234D",                   ExpectedResult = true)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0",                     ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0-alpha1+234F234D",                     ExpectedResult = false)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0-alpha1+234F234D",     ExpectedResult = true)]
        public bool TestSemVerEquality(string version, string otherVersion)
        {
            SemVer semVer = SemVer.Parse(version);
            SemVer semVer2 = SemVer.Parse(otherVersion);
            return semVer == semVer2;
        }

        [Test]
        [TestCase("0.9.0", "1.0.0",                                     ExpectedResult = true)]
        [TestCase("0.9.0-alpha1", "1.0.0",                              ExpectedResult = true)]
        [TestCase("1.0.1", "1.0.0",                                     ExpectedResult = false)]
        [TestCase("1.1.0-alpha1", "1.0.0",                              ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0",                                     ExpectedResult = false)]
        [TestCase("1.0.0-alpha1", "1.0.0",                              ExpectedResult = true)]
        [TestCase("1.0.0", "1.0.0-alpha1",                              ExpectedResult = false)]
        [TestCase("1.0.0-alpha1", "1.0.0-alpha1",                       ExpectedResult = false)]
        [TestCase("1.0.0+234F234D", "1.0.0",                            ExpectedResult = true)]
        [TestCase("1.0.0", "1.0.0+234F234D",                            ExpectedResult = false)]
        [TestCase("1.0.0+234F234D", "1.0.0+234F234D",                   ExpectedResult = false)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0",                     ExpectedResult = true)]
        [TestCase("1.0.0", "1.0.0-alpha1+234F234D",                     ExpectedResult = false)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0-alpha1+234F234D",     ExpectedResult = false)]
        public bool TestSemVerIsOlderThan(string version, string otherVersion)
        {
            SemVer semVer = SemVer.Parse(version);
            SemVer semVer2 = SemVer.Parse(otherVersion);
            return semVer < semVer2;
        }

        [Test]
        [TestCase("0.9.0", "1.0.0",                                     ExpectedResult = true)]
        [TestCase("0.9.0-alpha1", "1.0.0",                              ExpectedResult = true)]
        [TestCase("1.0.1", "1.0.0",                                     ExpectedResult = false)]
        [TestCase("1.1.0-alpha1", "1.0.0",                              ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0",                                     ExpectedResult = true)]
        [TestCase("1.0.0-alpha1", "1.0.0",                              ExpectedResult = true)]
        [TestCase("1.0.0", "1.0.0-alpha1",                              ExpectedResult = false)]
        [TestCase("1.0.0-alpha1", "1.0.0-alpha1",                       ExpectedResult = true)]
        [TestCase("1.0.0+234F234D", "1.0.0",                            ExpectedResult = true)]
        [TestCase("1.0.0", "1.0.0+234F234D",                            ExpectedResult = false)]
        [TestCase("1.0.0+234F234D", "1.0.0+234F234D",                   ExpectedResult = true)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0",                     ExpectedResult = true)]
        [TestCase("1.0.0", "1.0.0-alpha1+234F234D",                     ExpectedResult = false)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0-alpha1+234F234D",     ExpectedResult = true)]
        public bool TestSemVerIsOlderOrEqualTo(string version, string otherVersion)
        {
            SemVer semVer = SemVer.Parse(version);
            SemVer semVer2 = SemVer.Parse(otherVersion);
            return semVer <= semVer2;
        }

        [Test]
        [TestCase("0.9.0", "1.0.0",                                     ExpectedResult = false)]
        [TestCase("0.9.0-alpha1", "1.0.0",                              ExpectedResult = false)]
        [TestCase("1.0.1", "1.0.0",                                     ExpectedResult = true)]
        [TestCase("1.1.0-alpha1", "1.0.0",                              ExpectedResult = true)]
        [TestCase("1.0.0-alpha1", "1.0.0",                              ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0-alpha1",                              ExpectedResult = true)]
        [TestCase("1.0.0-alpha1", "1.0.0-alpha1",                       ExpectedResult = false)]
        [TestCase("1.0.0+234F234D", "1.0.0",                            ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0+234F234D",                            ExpectedResult = true)]
        [TestCase("1.0.0+234F234D", "1.0.0+234F234D",                   ExpectedResult = false)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0",                     ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0-alpha1+234F234D",                     ExpectedResult = true)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0-alpha1+234F234D",     ExpectedResult = false)]
        public bool TestSemVerIsNewerThan(string version, string otherVersion)
        {
            SemVer semVer = SemVer.Parse(version);
            SemVer semVer2 = SemVer.Parse(otherVersion);
            return semVer > semVer2;
        }

        [Test]
        [TestCase("0.9.0", "1.0.0",                                     ExpectedResult = false)]
        [TestCase("0.9.0-alpha1", "1.0.0",                              ExpectedResult = false)]
        [TestCase("1.0.1", "1.0.0",                                     ExpectedResult = true)]
        [TestCase("1.1.0-alpha1", "1.0.0",                              ExpectedResult = true)]
        [TestCase("1.0.0", "1.0.0",                                     ExpectedResult = true)]
        [TestCase("1.0.0-alpha1", "1.0.0",                              ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0-alpha1",                              ExpectedResult = true)]
        [TestCase("1.0.0-alpha1", "1.0.0-alpha1",                       ExpectedResult = true)]
        [TestCase("1.0.0+234F234D", "1.0.0",                            ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0+234F234D",                            ExpectedResult = true)]
        [TestCase("1.0.0+234F234D", "1.0.0+234F234D",                   ExpectedResult = true)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0",                     ExpectedResult = false)]
        [TestCase("1.0.0", "1.0.0-alpha1+234F234D",                     ExpectedResult = true)]
        [TestCase("1.0.0-alpha1+234F234D", "1.0.0-alpha1+234F234D",     ExpectedResult = true)]
        public bool TestSemVerIsNewerOrEqualTo(string version, string otherVersion)
        {
            SemVer semVer = SemVer.Parse(version);
            SemVer semVer2 = SemVer.Parse(otherVersion);
            return semVer >= semVer2;
        }
    }
}
