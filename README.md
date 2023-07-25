# Hyperswitch HTML + Dotnet(.NET) Integration

Build a simple checkout web-app to collect payment details and make a dummy payment. Included are some basic build and run scripts you can use to run the demo application.

## Introduction

This demo application uses the following tech-stack :

**Frontend :** `HTML + CSS` with `JavaScript`

**Backend :** `.NET`  

## Prerequisites

Before running the demo app, please make sure to activate your Hyperswitch secret keys (API Key and Publishable Key) in your [Hyperswitch Dashboard](https://app.hyperswitch.io/developers). 

Don't have a Hyperswitch account? [Sign up here!](https://app.hyperswitch.io/register) 

## Running the sample

1. Add your keys :
    - Navigate to `public/script.js` and replace the placeholder `HYPERSWITCH_PUBLISHABLE_KEY` with your publishable key.
    - Navigate to `server.cs` and replace the placeholder `HYPERSWITCH_API_KEY` with your API key.

2. Install the dependencies / build the server :
~~~
dotnet restore
~~~

3. Run the server :
~~~
dotnet run
~~~

4. The sample app will now be accessible here : [http://localhost:4242/index.html](http://localhost:4242/index.html).
