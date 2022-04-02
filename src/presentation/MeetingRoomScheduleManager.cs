using MeetingRoomScheduler.Model;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MeetingRoomScheduler
{
    public class MeetingRoomScheduleManager
    {
        private readonly HttpClient _httpClient;

        public MeetingRoomScheduleManager()
        {
            _httpClient = new HttpClient();
        }
        public void Run()
        {
            /*
            //asks if you want to see scheduled meetings
            Console.WriteLine("Whould you like to see scheduled meetings? (Y/N)");

            var answer = Console.ReadLine();
            if (answer.ToUpper() == "Y")
            {
                Console.WriteLine("Scheduled meetings");
                Console.WriteLine("------------------");
                var response = _httpClient.Send(new HttpRequestMessage { RequestUri = new Uri("https://localhost:7006/Schedule") , Method = HttpMethod.Get});
                var meetings = JsonSerializer.Deserialize<List<string>>(response.Content.ReadAsStream());
                foreach (var meeting in meetings)
                {
                    Console.WriteLine(meeting);
                    Console.WriteLine("------------------");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("Welcome to Meeting room scheduler");
            Console.WriteLine("Write meeting day");
            
            var day = string.Empty;
            while (string.IsNullOrWhiteSpace(day))
            {
                //reads day as input
                var input = Console.ReadLine();
                Regex regex = new Regex(@"^2[0-1][0-9][0-9](-)[0-1][0-9](-)[0-3][0-9]", RegexOptions.IgnorePatternWhitespace);
                //new Regex(@"^(^0?[1-9]$)|(^1[0-2]$)$", RegexOptions.IgnorePatternWhitespace);
                Match x = regex.Match(input);
                //applies regex to input
                if(x.Success)
                {
                    day = input;
                }
                else
                {
                    Console.WriteLine("Incorrect date your should follow yyyy-MM-dd fortmat");
                }
            }
            Console.WriteLine("Your selected day is " + day);
            Console.WriteLine("Write meeting time");
            
            var time = string.Empty;
            while (string.IsNullOrWhiteSpace(time))
            {
                //reads time as innput
                var input = Console.ReadLine();
                Regex regex = new Regex(@"^[0-6]\d:[0-6]\d$", RegexOptions.IgnorePatternWhitespace);
                Match x = regex.Match(input);
                //applies regex to input
                if (x.Success)
                {
                    time = input;
                }
                else
                {
                    Console.WriteLine("Incorrect time your should follow hh:mm fortmat");
                }
            }
            Console.WriteLine("Your selected time is " + time);
            Console.WriteLine("Select a room from the following");
            //get meeting rooms
            var meetingRoomsResponse = _httpClient.Send(
                new HttpRequestMessage
                {
                    RequestUri = new Uri("https://localhost:7006/Stance"),
                    Method = HttpMethod.Get
                });
            var roomList = JsonSerializer.Deserialize<List<string>>(meetingRoomsResponse.Content.ReadAsStream())
                .Select((x,i)=>i + 1 + "-" + x)
                .ToList();
            foreach (var roomName in roomList)
            {
                Console.WriteLine(roomName);
            }
            //selects room by ordered index
            var selectedIndex = -1;
            while (!(selectedIndex > 0))
            {
                try
                {
                   var input = Console.ReadLine();
                    if(!int.TryParse(input, out selectedIndex) || !roomList.Any(x=>x.Contains(input.ToString())))
                    {
                        selectedIndex = -1;
                    }
                }
                catch 
                { 
                    Console.WriteLine("Wrong room index try again");
                }
            }
            var room = roomList[selectedIndex - 1].Split("-")[1];
            Console.WriteLine("Your selected room is " + room);

            Console.WriteLine("Input peoples email");
            var people = Console.ReadLine().Split(", ");
            */
            var x = Console.ReadKey();
            var day = "2022-05-15";
            var time = "09:30";
            var people = new List<string>
            {
                "erika.berger@millenium-mag.com",
                "lsalander@miltonsecurity.com"
            };
            var room = "Glass infinity view";
            var date = DateTime.ParseExact(day + " " + time, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            //sends scheduler request
            var meetingScheduledResponse = _httpClient.Send(
                new HttpRequestMessage
                {
                    RequestUri = new Uri("https://localhost:7006/Schedule"),
                    Method = HttpMethod.Post,
                    Content = new StringContent(JsonSerializer.Serialize(
                        new MeetingRoomSchedule
                        {
                            Date = date,
                            Emails = people,
                            Title = "TBD",
                            StanceName = room
                        }),Encoding.UTF8,"application/json") 
                    });

            try
            {
                using (var streamReader = new StreamReader(meetingScheduledResponse.Content.ReadAsStream(), Encoding.UTF8))
                {
                    var content = streamReader.ReadToEnd();
                    var scheduledMeeting = JsonSerializer.Deserialize<MeetingRoomSchedule>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    Console.WriteLine("Your scheduled meeting is:");
                    Console.WriteLine(scheduledMeeting.StanceName + " at " + scheduledMeeting.Date + " with " + string.Join(",", scheduledMeeting.Emails));
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine("Something went wrong");
            }
        }
    }
}
