var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var assignments = new List<AssignmentDto>
{
    new(1, "Bai tap REST API", "Thiet ke endpoint REST API", true),
    new(2, "Bai tap Client", "Viet console client ket noi API", true)
};

var submissions = new List<SubmissionDto>();

app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        service = "Submission.Api",
        status = "running"
    });
});

app.MapGet("/api/assignments", () =>
{
    return Results.Ok(assignments);
});

// API lấy chi tiết bài tập theo ID
app.MapGet("/api/assignments/{assignmentId:int}", (int assignmentId) =>
{
    var assignment = assignments.FirstOrDefault(
        item => item.Id == assignmentId
    );

    return assignment is null
        ? Results.NotFound(new ApiError(
            "ASSIGNMENT_NOT_FOUND",
            "Khong tim thay bai tap."
        ))
        : Results.Ok(assignment);
});

app.MapPost("/api/assignments/{assignmentId:int}/submissions",
    (int assignmentId, CreateSubmissionRequest request) =>
{
    var assignment = assignments.FirstOrDefault(
        item => item.Id == assignmentId
    );

    if (assignment is null)
    {
        return Results.NotFound(new ApiError(
            "ASSIGNMENT_NOT_FOUND",
            "Khong tim thay bai tap."
        ));
    }

    if (!assignment.IsOpen)
    {
        return Results.BadRequest(new ApiError(
            "ASSIGNMENT_CLOSED",
            "Bai tap da dong."
        ));
    }

    if (string.IsNullOrWhiteSpace(request.StudentCode) ||
        string.IsNullOrWhiteSpace(request.RepositoryUrl))
    {
        return Results.BadRequest(new ApiError(
            "INVALID_SUBMISSION",
            "Ma sinh vien va Repository URL khong duoc de trong."
        ));
    }

    var submission = new SubmissionDto(
        Guid.NewGuid(),
        assignmentId,
        request.StudentCode.Trim(),
        request.RepositoryUrl.Trim(),
        "received",
        DateTimeOffset.UtcNow
    );

    submissions.Add(submission);

    return Results.Created(
        $"/api/submissions/{submission.Id}",
        submission
    );
});

// API lấy thông tin bài nộp theo ID
app.MapGet("/api/submissions/{submissionId:guid}", (Guid submissionId) =>
{
    var submission = submissions.FirstOrDefault(
        item => item.Id == submissionId
    );

    return submission is null
        ? Results.NotFound(new ApiError(
            "SUBMISSION_NOT_FOUND",
            "Khong tim thay bai nop."
        ))
        : Results.Ok(submission);
});

app.Run();

record AssignmentDto(
    int Id,
    string Title,
    string Description,
    bool IsOpen
);

record ApiError(string Code, string Message);
record CreateSubmissionRequest(
    string StudentCode,
    string RepositoryUrl
);

record SubmissionDto(
    Guid Id,
    int AssignmentId,
    string StudentCode,
    string RepositoryUrl,
    string Status,
    DateTimeOffset ReceivedAt
);