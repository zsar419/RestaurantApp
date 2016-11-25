# RestaurantApp
Cross-platform mobile restaurant app developed for MSA programme using Xamarin and Visual Studio

<center><strong>Fabtikam Foods online ordering service</strong></center>

<strong>Features:</strong>
<ul>
  <li>Basic login and registration system via user database</li>
  <li>Retrieve and display food items with thumbnails from database</li>
  <li>Allows authenticated users to add food items to cart, place and cancel orders via database</li>
  <li>Food items recomended based on the weather of current user location (retrieved via GPS)</li>
  <li>Microsoft bot framework integrated with LUIS API to query desired food items in store via text/language</li>
  <ul>
    <li>E.g: Display all "cold" "drinks" on "sale" less than "4 dollars"</li>
    <li>This will display all food of category=drinks, type=cold, sale=true and price lt $4<li>
    <li>Integrated into App using webview - shows image cards of items retrieved</li>
  </ul>
  <li>Customized profile loading such that upon login, users name and profile picture displayed in navigation menu</li>
  <li>Ability to change user details - e.g. name, email, password, profile pic, address, phone</li>
  <li>Map page which shows current user location and location of Fabrikam Food store</li>
  <li>Basic facebook integration (however not used in database authentication)<li>
</ul>

<strong>API:</strong>
<ul>
  <li>Xamarin Google Maps API - <a>https://developer.xamarin.com/guides/xamarin-forms/user-interface/map/</a></li>
  <ul>
    <li>User location</li>
  </ul>
  <li>Weather API - <a>https://openweathermap.org/current</a></li>
  <ul>
    <li>Temperature at user location, name (town) of user location</li>
  </ul>
  <li>LUIS API - <a>https://www.microsoft.com/cognitive-services/en-us/language-understanding-intelligent-service-luis</a></li>
  <ul>
    <li>Querying od database items based on natural language text input - done by bot</li>
  </ul>
  <li>Imgur API - <a>https://api.imgur.com/endpoints/image</a></li>
  <ul>
    <li>User profile image hosting services (profile picture)</li>
  </ul>
</ul>

<strong>Resources:</strong>
<ul>
  <li>Cross-platform Geolocator service (GPS integration) - <a>https://github.com/jamesmontemagno/GeolocatorPlugin</a></li>
  <ul>
    <li>Allows cross-platform access to device GPS in Xamarin forms</li>
  </ul>
  <li>Cross-platform Camera/Media service (Photo/image integration) - <a>https://github.com/jamesmontemagno/MediaPlugin</a></li>
  <ul>
    <li>Allows cross-platform access to camera and local images (file system) in Xamarin forms</li>
  </ul>
  <li>Microsoft Bot framework - <a>https://dev.botframework.com/</a></li>
  <ul>
    <li>Seemlessly integrated bot which makes API calls to LUIS and displays food item details within webview via natural language text input</li>
  </ul>
</ul>
