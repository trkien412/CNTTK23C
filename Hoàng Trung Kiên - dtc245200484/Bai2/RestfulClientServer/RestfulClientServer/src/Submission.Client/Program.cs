
using System.Net.Http.Json;

using var httpClient = new HttpClient
{
    BaseAddress = new Uri("http://localhost:5000")
};

// 1. Lấy danh sách bài tập
Console.WriteLine("Dang lay danh sach bai tap...");

var assignments = await httpClient.GetFromJsonAsync<List<AssignmentDto>>(
    "/api/assignments"
);

if (assignments is not null)
{
    foreach (var assignment in assignments)
    {
        Console.WriteLine($"ID: {assignment.Id}");
        Console.WriteLine($"Ten bai tap: {assignment.Title}");
        Console.WriteLine($"Mo: {assignment.IsOpen}");
        Console.WriteLine("-------------------------");
    }
}

// 2. Xem chi tiết bài tập
Console.Write("\nNhap ID bai tap can xem: ");
var input = Console.ReadLine();

if (int.TryParse(input, out int assignmentId))
{
    var assignment = await httpClient.GetFromJsonAsync<AssignmentDto>(
        $"/api/assignments/{assignmentId}"
    );

    if (assignment is not null)
    {
        Console.WriteLine($"Ten: {assignment.Title}");
        Console.WriteLine($"Mo: {assignment.IsOpen}");
        Console.WriteLine($"Mo ta: {assignment.Description}");
    }
}
else
{
    Console.WriteLine("ID khong hop le!");
}

// 3. Gửi bài nộp
Console.WriteLine("\n--- GUI BAI NOP ---");

Console.Write("Nhap ma sinh vien: ");
var studentCode = Console.ReadLine();

Console.Write("Nhap Repository URL: ");
var repositoryUrl = Console.ReadLine();

var request = new CreateSubmissionRequest(
    studentCode ?? "",
    repositoryUrl ?? ""
);

var postResponse = await httpClient.PostAsJsonAsync(
    $"/api/assignments/{assignmentId}/submissions",
    request
);

Console.WriteLine($"HTTP Status: {(int)postResponse.StatusCode}");

if (postResponse.IsSuccessStatusCode)
{
    var submission = await postResponse.Content
        .ReadFromJsonAsync<SubmissionDto>();

    if (submission is not null)
    {
        Console.WriteLine($"Submission ID: {submission.Id}");
        Console.WriteLine($"Student Code: {submission.StudentCode}");
        Console.WriteLine($"Status: {submission.Status}");
        Console.WriteLine($"Received At: {submission.ReceivedAt}");

        // 4. Tra cứu bài nộp vừa tạo
        var savedSubmission = await httpClient.GetFromJsonAsync<SubmissionDto>(
            $"/api/submissions/{submission.Id}"
        );

        if (savedSubmission is not null)
        {
            Console.WriteLine("\n--- THONG TIN BAI NOP ---");
            Console.WriteLine($"ID: {savedSubmission.Id}");
            Console.WriteLine($"Student: {savedSubmission.StudentCode}");
            Console.WriteLine($"Repository: {savedSubmission.RepositoryUrl}");
            Console.WriteLine($"Status: {savedSubmission.Status}");
        }
    }
}
else
{
    var error = await postResponse.Content.ReadAsStringAsync();
    Console.WriteLine($"Loi: {error}");
}
