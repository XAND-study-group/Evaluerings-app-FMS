using Microsoft.AspNetCore.Authorization;
using Module.Feedback.AuthorizationHandlers;

namespace Evaluering.API.Extensions;

public static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddAuthorizationWithPolicies(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAuthorization(options =>
        {
            #region EvaluationApp policies

            options.AddPolicy("ReadFeedback",
                policy => policy.RequireClaim("Permission", "ReadFeedback"));
            options.AddPolicy("ReadInteractedFeedback",
                policy => policy.RequireClaim("Permission", "ReadInteractedFeedback"));
            options.AddPolicy("PostFeedback",
                policy => policy.RequireClaim("Permission", "PostFeedback"));
            options.AddPolicy("CommentOnFeedback",
                policy => policy.RequireClaim("Permission", "CommentOnFeedback"));
            options.AddPolicy("AnswerExitSlip",
                policy => policy.RequireClaim("Permission", "AnswerExitSlip"));
            options.AddPolicy("ReadExitSlipAnswers",
                policy => policy.RequireClaim("Permission", "ReadExistSlipAnswers"));
            options.AddPolicy("CreateExitSlips",
                policy => policy.RequireClaim("Permission", "CreateExistSlips"));
            options.AddPolicy("PrintExitSlipReport",
                policy => policy.RequireClaim("Permission", "PrintExitSlipReport"));
            options.AddPolicy("PrintFeedbackReport",
                policy => policy.RequireClaim("Permission", "PrintFeedbackReport"));
            options.AddPolicy("VoteOnFeedback",
                policy => policy.RequireClaim("Permission", "VoteOnFeedback"));
            options.AddPolicy("RoomManagement",
                policy => policy.RequireClaim("Permission", "RoomManagement"));
            options.AddPolicy("ReadRoom",
                policy => policy.RequireClaim("Permission", "ReadRoom"));

            options.AddPolicy("AssureUserInRoomOfFeedbackCreate",
                policy => policy.Requirements.Add(new AssureUserInRoomOfFeedbackCreateRequirement("Admin")));
            options.AddPolicy("AssureUserInRoomOfFeedbackModification",
                policy => policy.Requirements.Add(new AssureUserInRoomOfFeedbackModificationRequirement("Admin")));

            #endregion

            #region School policies

            options.AddPolicy("Admin",
                policy => policy.RequireClaim("Role", "Admin"));
            options.AddPolicy("AdminOrTeacher",
                policy => policy.RequireClaim("Role", "Admin", "Teacher"));
            options.AddPolicy("Teacher",
                policy => policy.RequireRole("Role", "Teacher"));
            options.AddPolicy("TeacherOrUser",
                policy => policy.RequireRole("Role", "Teacher", "User"));
            options.AddPolicy("User",
                policy => policy.RequireRole("Role", "User"));

            #endregion
        });

        return serviceCollection
            .AddScoped<IAuthorizationHandler, AssureUserInRoomOfFeedbackCreate>()
            .AddScoped<IAuthorizationHandler, AssureUserInRoomOfFeedbackModification>();
    }
}