# VSA Journey 🎖️

In this repository, I will be building a simple project to master VSA (Vertical Slice Architecture) as well as CQRS pattern together witth Mediator Pattern using MediatR.

I've read several articles, watched several videos about the vertical slice architecture so I thought why not build a project around it. This will be a simple but elegant projectt.

I plan to explore and cut through several parts of building robust APIs In ASP.Net that I haven't covered before. They include

1. Robust Authorization (RBAC) Role Based Access Control. Microsoft Identity or JWT
2. Structured Logging with Serilog. I recently learned this.
3. Middlewares. Plugging MediatR in validation pipelines 
4. Background Jobs. Hangfire and Redis. Might explore RabbitMQ later.
5. Mailing service.
6. Unit Testing and IE testing with Xunit
7. API Docs with Swagger
8. I might also play with S3 for file uploads.
9. Docker for containarization and deployment. Might also explore Github actions.
10. I might also build a frontend app for this with React, NextJs, Methane Cli<https://github.com/adedoyin-emmanuel/methane-cli> and ShadcnUi. Although my focus is on building the backend.
11. If I reach this point, I will do my best to deploy on Azure. I tried it before, I was a noob then, it should be easier now.

I know it wouldn't be easy but I will achieve this. Once I can get this, I will continue to focus on architectural patterns, DSA and best practises in building enterprise softwares.
I will also attach links to videos, articles etc that will help me build this. I know 90% will come from Milan Jovanović's articles or videos, he is a great instructor. I'm learning C# / ASP.NET Core best practises as 
well as Architectural and Design Patterns from him. You can check him out here. <https://www.milanjovanovic.tech> <https://youtube.com/@MilanJovanovicTech>


## The Problem ⚠️

That was interesting and I'm sure you've asked yourself what is this guy building? I'm going to **FolyCare**. I am building this project out of pain to address the major issue I face when I visit a superstore
around my place called **Folyma**. **Folyma** is a pretty big superstore that sells almost any kind of groceries and pharmaceuticals. They have market-friendly prices and it is located on a major road. 
Due to these factors, they have several customers. While that is good, it also introduces delays and long queue which can sometimes be annoying especially if you're getting something of about `500 Naria`, 
you've to queue. Sometimes, customers get frustrated and leave. Now, imagine you're able to order what you want and all you do is to issue a ticket to pick your product or better still, it gets delivered to 
your place if you live around the area. This is what **FolyCare** is going to solve. 


## The Solution 🔧

I'm building **FolyCare** to address this issue. **FolyCare** makes shopping at **Folyma** superstore faster and stress-free! Instead of standing in long queues, you can order your groceries and medications online ahead of time. 
Once your order is ready, you’ll get a ticket to pick it up at a specific time, skipping the wait entirely. If you prefer, FolyCare can also deliver your items straight to your doorstep.
It’s all about saving you time and making life easier—no more frustration, no more delays. Whether you’re buying a small item or stocking up for the week, FolyCare is here to ensure you shop smarter and more conveniently.

## Key Features of FolyCare 🧰
1. User Management
2. Product Catalog
3. Order Placement
4. Payment System 
5. Pickup Management 
6. Delivery System 
7. Reliable delivery with live tracking and status updates. 
8. Notifications 
9. Reviews & Feedback 
10. Reports & Analytics


## Resouces 🛠️

1. **Structured Logging**: This is a logging practice where you apply the same log format for all logs. This can help us analyze our logs and treat them like dataset
    Milan did a good job explaining how to integrate **Serilog** into our **ASP.NET** web APIS using **MediatR** behaviour pipeline. <https://www.youtube.com/watch?v=nVAkSBpsuTk>, <https://www.youtube.com/watch?v=JVX9MMpO6pE>,
    <https://www.milanjovanovic.tech/blog/structured-logging-in-asp-net-core-with-serilog>
2. **Using Identity And JWT For Your API Authentication**: I was a bit confused on how to use both Identity and JWT for my Authentication and Authorization. The following videos and articles helped me understand how to.
    <https://www.youtube.com/watch?v=8J3nuUegtL4> <https://medium.com/@madu.sharadika/authentication-and-authorization-in-net-web-api-with-jwt-b46ef2f54e31>, <https://patelalpeshn.medium.com/jwt-authentication-and-authorization-in-net-6-0-with-identity-framework-3da2ac05e3c5>
    <https://www.youtube.com/watch?v=qPlqUQdf0fE>, <https://www.youtube.com/watch?v=99-r3Y48SYE>
3. **Structuring Your Program.cs**: Gui Ferreria did a good job explaining how to achieve this. <https://youtu.be/pj0hqTlxUX0?si=qGNqXb3OtPiRqm8h>,
   Amichai Mantinband also helped me better understand it. You can watch the video here <https://www.youtube.com/watch?v=0PCFdmb7kxo>. After implementing it in my API, I wrote a twitter thread about it. Check it out <https://x.com/Emmysoft_Tm/status/1871459071569879503>
4. **Consistent API Response**: I've been doing this for quite sometime now but in this project I decided to make it better. I created an **ApiResponse** class with popular Http Methods. I then registed this class in my Service Extension. From my controllers, I can inject the ApiResponse class and use it in any method that implements the **IActionResult**. You can read how I achieved that here <https://x.com/Emmysoft_Tm/status/1871462562690449884>
5. **VSA (Vertical Slice Architecture)**: The Vertical Slice Architecture also referred to the Features pattern is design architecture in which all files for a single use are grouped inside a folder So, the cohesion for a single use case is very high. This simplifies the development experience. It's easy to find all the relevant components for each feature since they are close together.
   Milan Jovanović explained how to implement it on his blog <https://www.milanjovanovic.tech/blog/vertical-slice-architecture?utm_source=LinkedIn&utm_medium=social&utm_campaign=02.12.2024>  You can also watch the video here <https://www.youtube.com/watch?v=msjnfdeDCmo>. The VSA is usually used with he **CQRS** pattern with **MediatR** Although I haven't worked with it. But I know I will understand it soon. All I know is that **CQRS** stands for Command Query Responsibility Segregation.  The CQRS pattern uses separate models for reading and updating data. The benefits of using CQRS are complexity management, improved performance, scalability, and security.
   Milan also wrote an article on that <https://www.milanjovanovic.tech/blog/cqrs-pattern-with-mediatr>. He also made a video explaining how. <https://youtu.be/vdi-p9StmG0>. Gui Ferreria also did a great job explaining it. <https://youtu.be/uImL4Ar_hyo?si=aa4Zs4BZkQ03gi6A>. Try reading the article and make sure you watch the video. Watch Milan's video first and then Gui Ferreria's next. I am sure you will understand how to structure your project when using the VSA.
    