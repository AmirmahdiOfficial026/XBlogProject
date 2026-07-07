using XBLog.Domain.Entities;

namespace XBlog.Application.ApplicationMessage;
public static class ResultMessage
{
    public static string NotFound = "There is an error in the system.";
    //Article Category
    public static string AddArticleCategory = "Category created successfully.";
    public static string UpdateArticleCategory = "Category updated successfully.";
    public static string RemoveArticleCategory = "Category successfully deleted.";
    public static string RestoreArticleCategory = "Category successfully retrieved.";
    public static string ArticleCategoryNotFound = "No category found with these specifications.";
    //Article
    public static string AddArticle = "Article created successfully.";
    public static string UpdateArticle = "Article updated successfully.";
    public static string RemoveArticle = "Article successfully deleted.";
    public static string RestoreArticle = "Article successfully retrieved.";
    public static string ArticleNotFound = "No Article found with these specifications.";
    //Comment
    public static string AddComment = "Comment created successfully.";
    public static string RemoveComment = "Comment successfully deleted.";
    public static string RejectComment = "Comment successfully rejected.";
    public static string ConfirmComment = "Comment successfully confirmed.";
    public static string CommentNotFound = "No Comment found with these specifications.";

}
