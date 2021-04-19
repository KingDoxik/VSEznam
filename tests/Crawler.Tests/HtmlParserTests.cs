using System;
using Common.Parser;
using Xunit;

namespace Crawler.Tests
{
    public class HtmlParserUnitTests
    {
        [Fact]
        public void Parse_EverythingContains_FullParserResult()
        {
            var parser = new HtmlParser();
            var html = @"
                <html>
                    <head>
                        <title>Test document</title>
                        <meta Name='keywords' content='test1, test2'>
                        <meta name='description' content='This is a test document'>
                        <meta name='author' content='Dominik Vít'>
                    </head>
                    <body>
                    </body>
                </html>
                ";
            var parserResult = parser.Parse(html);

            Assert.Equal("Test document", parserResult.Title);
            Assert.Equal("Dominik Vít", parserResult.Metadata["author"]);
            Assert.Equal("This is a test document", parserResult.Metadata["description"]);
            Assert.Equal("test1, test2", parserResult.Metadata["keywords"]);
        }

        [Fact]
        public void Parse_MissingTitleContainsH1_TitleIsH1()
        {
            var parser = new HtmlParser();
            var html = @"
                <html>
                    <head>
                        <meta name='keywords' content='test1, test2'>
                        <meta name='description' content='This is a test document'>
                        <meta name='author' content='Dominik Vít'>
                    </head>
                    <body>
                        <h1>Test document</h1>
                    </body>
                </html>
                ";
            var parserResult = parser.Parse(html);

            Assert.Equal("Test document", parserResult.Title);
        }

        [Fact]
        public void Parse_HasMultipleLinks_AllLinksAreInParserResult()
        {
            var parser = new HtmlParser();
            var html = @"
                <html>
                    <head>
                        <meta name='keywords' content='test1, test2'>
                        <meta name='description' content='This is a test document'>
                        <meta name='author' content='Dominik Vít'>
                    </head>
                    <body>
                        <a href='https://link1.com'>Link1</a>
                        <a href='https://link2.com'>Link2</a>
                        <a href='https://link1.com'>Link3</a>
                    </body>
                </html>
                ";

            var parserResult = parser.Parse(html);

            Assert.Equal(2, parserResult.ContainedUrls.Count);

            Assert.Collection(parserResult.ContainedUrls,
                item => Assert.Equal("link1.com", item.Host),
                item => Assert.Equal("link2.com", item.Host)
            );
        }
    }
}
