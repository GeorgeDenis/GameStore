#  Steam-like Game Store
<div align="justify">
  <p>
 &emsp;A fully-featured Steam-inspired game store where users can explore a wide range of games, filter by price, release date, and genre, view detailed game pages, post reviews, and manage their profiles. While users can add games to their accounts, no real payment processing is implemented. The platform supports switching between multiple currencies (EUR, USD, RON), and notifications are automatically cleaned up after 24 hours via background jobs.
Admins can manage the game catalog with the ability to add, update, or delete games.
  </p>
</div>
## Features
###  🎮 Game Store
-  Storefront: Browse all available games.
-  Search & Filters: Filter games by price, release date, and genres.
-  Currency Switch: Switch between EUR, USD, and RON.
-  Game Pages: Each game has a dedicated page with detailed descriptions.
-  Reviews: Post reviews for any game.
###  🧑‍💻 User Profile
-  Profile Management: Create and manage your user profile.
-  Game Library: Games added from the store are saved to your account.
###  🔔 Notifications
-  Real-time Notifications: Receive notifications within the platform.
-  Auto-Cleanup: Old read notifications are automatically deleted after 24 hours using Hangfire.
###  🛠 Admin Module
-  Game Management: Admins can add, update, and delete games in the store.

##  Stack
-  Backend: .NET 8
-  Frontend: Angular 16
-  Database: MySQL Server
-  Background Jobs: Hangfire for automatic deletion of old notifications.
