using LearningApplication1;

Instagram ac1 = new Instagram("acc1", 200, 3);
Instagram ac1 = new Instagram("acc2", 300, 4);
Instagram ac1 = new Instagram("acc3", 400, 5);
Instagram ac1 = new Instagram("acc4", 500, 6);
Instagram[] arr = new Instagram[5] {ac1,ac2,ac3,ac4};

List<Instagram> iglist=new List<Instagram>() { ac1, ac2, ac3, ac4 };

Console.WriteLine(arr);

for(int i = 0; i < arr.Length; i++)
{
    Console.WriteLine($"Username: {arr[i].UserName}");
    Console.WriteLine($"Post Count: {arr[i].postCount}");

    Console.WriteLine($"Followers: {arr[i].followers}");
    Console.WriteLine("================");


}
foreach (Instagram instagram in iglist)
{
    Console.WriteLine($"Username: {arr[i].UserName}");
    Console.WriteLine($"Post Count: {arr[i].postCount}");

    Console.WriteLine($"Followers: {arr[i].followers}");
    Console.WriteLine("================");
}
