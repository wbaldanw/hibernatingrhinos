using System;
using System.Collections.Generic;
using FluentMapping.Domain.Scenario3;
using FluentNHibernate.Framework;
using Iesi.Collections.Generic;
using NHibernate;
using NUnit.Framework;

namespace FluentMapping.UnitTests.Scenario3
{
    public class Blog_Fixture : FixtureBase
    {
        protected Person author;

        protected override void CreateInitialData(ISession session)
        {
            base.CreateInitialData(session);
            author = new Person {FirstName = "Gabriel", LastName = "Schenker"};
            session.Save(author);
        }
    }

    [TestFixture]
    public class When_no_blog_exists : Blog_Fixture
    {
        [Test]
        public void Can_add_new_blog()
        {
            var blog = new Blog {Name = "Gabriel's Blog", Author = author};
            Session.Save(blog);
            Session.Flush();
            Session.Clear();

            var fromDb = Session.Get<Blog>(blog.Id);

            fromDb.ShouldNotBeNull();
            fromDb.ShouldNotBeTheSameAs(blog);
            fromDb.Id.ShouldEqual(blog.Id);
            fromDb.Name.ShouldEqual(blog.Name);
            fromDb.Author.ShouldNotBeNull();
            fromDb.Author.Id.ShouldEqual(blog.Author.Id);
        }

        [Test]
        public void Can_add_new_blog_revisited()
        {
            new PersistenceSpecification<Blog>(Session)
                .CheckProperty(x => x.Name, "Gabriel's Blog")
                .CheckProperty(x => x.Author, author)
                .VerifyTheMappings();
        }
    }

    [TestFixture]
    public class When_a_blog_exists : Blog_Fixture
    {
        private Blog blog;

        protected override void CreateInitialData(ISession session)
        {
            base.CreateInitialData(session);
            blog = new Blog {Name = "Gabriel's Blog", Author = author};
            session.Save(blog);
        }

        [Test]
        public void Can_add_post_to_blog()
        {
            var post = new Post
                        {
                            Title = "First Post",
                            Body = "Just a test",
                            PublicationDate = DateTime.Today
                        };
            blog.Posts.Add(post);
            Session.Update(blog);

            Session.Flush();
            Session.Clear();

            var fromDb = Session.Get<Blog>(blog.Id);

            fromDb.Posts.Count.ShouldEqual(1);
            fromDb.Posts.First().Id.ShouldEqual(post.Id);
        }

        [Test]
        public void Can_add_post_to_blog_revisited()
        {
            List<Post> posts = new List<Post>();
            posts.AddRange(new[]
                            {
                                new Post {
                                            Title = "First Post",
                                            Body = "Just a test",
                                            PublicationDate = DateTime.Today
                                         },
                                new Post {
                                            Title = "Second Post",
                                            Body = "Just another test",
                                            PublicationDate = DateTime.Today.AddDays(-1)
                                         },
                            });

            new PersistenceSpecification<Blog>(Session)
                .CheckProperty(x => x.Name, "Gabriel's Blog")
                .CheckProperty(x => x.Author, author)
                .CheckList(x => x.Posts, posts)
                .VerifyTheMappings();
        }
    }

    [TestFixture]
    public class When_a_blog_with_a_post_exists : Blog_Fixture
    {
        private Blog blog;
        private Post post;
        private Comment comment;

        protected override void CreateInitialData(ISession session)
        {
            base.CreateInitialData(session);
            blog = new Blog { Name = "Gabriel's Blog", Author = author };
            post = new Post
                       {
                           Title = "First Post",
                           Body = "Just a test",
                           PublicationDate = DateTime.Today
                       };
            blog.Posts.Add(post);
            session.Save(blog);

            comment = new Comment("This is my comment", DateTime.Today, "someone@gmail.com");
        }

        [Test]
        public void Can_add_comment_to_post()
        {
            var thePost = Session.Get<Post>(post.Id);
            thePost.Comments.Add(comment);
            
            Session.Flush();
            Session.Clear();

            var fromDb = Session.Get<Post>(post.Id);
            fromDb.Comments.Count.ShouldEqual(1);
            fromDb.Comments.First().Equals(comment);
        }

        [Test]//[Ignore(".CheckList is not yet working")]
        public void Can_add_comment_to_post_revisited()
        {
            new PersistenceSpecification<Post>(Session)
                .CheckProperty(x => x.Title, "Some title")
                .CheckProperty(x => x.Body, "Some text")
                .CheckProperty(x => x.PublicationDate, DateTime.Today)
                .CheckComponentList(x => x.Comments, new[] { comment })
                .VerifyTheMappings();
        }
    }
}