Admin Login Credentials:
 Name: superadmin@admin.com
 Password: 123qwe!@#QWE

Wstęp
Projekt "Wypożyczalnia książek" jest aplikacją webową opartą na technologii ASP .NET MVC. Celem projektu jest stworzenie systemu, który umożliwi użytkownikom wypożyczanie w ramach wypożyczalni. W ramach projektu zostanie zaimplementowana funkcjonalność zarządzania książkami, klientami, wypożyczeniami oraz obsługi logowania i rejestracji użytkowników.


Technologie
Projekt zostanie zrealizowany z wykorzystaniem następujących technologii:
•	ASP .NET MVC: Jako framework do budowy aplikacji webowych, który umożliwi rozdzielenie logiki biznesowej od warstwy prezentacji oraz obsługę żądań HTTP.
•	Entity Framework: Jako framework ORM (Object-Relational Mapping) do obsługi bazy danych, który pozwoli na komunikację z bazą danych w sposób obiektowy.
•	C#: Jako główny język programowania, który będzie wykorzystywany do implementacji logiki biznesowej i obsługi zdarzeń.
•	HTML, CSS, JavaScript: Jako technologie frontendowe, które posłużą do tworzenia interfejsu użytkownika.

Architektura
Projekt zostanie zrealizowany w oparciu o architekturę MVC (Model-View-Controller), która pozwoli na rozdzielenie logiki biznesowej, prezentacji oraz obsługi żądań HTTP. W ramach projektu zostaną utworzone następujące komponenty:
•	Model: Odpowiadający za reprezentację danych w systemie, takie jak książki, klienci, wypożyczenia itp. Dane będą przechowywane w bazie danych przy użyciu Entity Framework.
•	Widok (View): Odpowiadający za prezentację danych użytkownikowi w postaci interfejsu graficznego. Widoki zostaną zaimplementowane przy użyciu języka Razor, który pozwala na integrację kodu C# z kodem HTML.
•	Kontroler (Controller): Odpowiadający za obsługę żądań HTTP oraz zarządzanie logiką biznesową. Kontrolery będą odpowiedzialne za odczyt i zapis danych z/do modelu oraz renderowanie odpowiednich widoków.

Model danych
•	Klasa „Book” - będzie reprezentować pojedynczą książkę w systemie. Będzie zawierać właściwości takie jak ID - identyfikator książki, Title - tytuł książki, Author - autor książki, IsAvailable - flaga informująca, czy książka jest dostępna do wypożyczenia oraz IsRemoved – flaga która wykonuje miękkie usuwanie książki.
•	Klasa „User” - będzie reprezentować klienta korzystającego z aplikacji. Będzie zawierać właściwości takie jak ID - identyfikator klienta, Email - adres email klienta, Hasło - hasło klienta oraz Wypożyczenia - kolekcja wypożyczeń przypisanych do danego klienta.
•	Klasa „Wypożyczenie” - będzie reprezentować pojedyncze wypożyczenie książki przez klienta. Będzie zawierać właściwości takie jak ID - identyfikator wypożyczenia,
 
RentDate - data wypożyczenia, ReturnDate - data zwrotu, BookID - książka, która została wypożyczona, UserID - klient, który wypożyczył książkę, IsAccepted – flaga inforumująca o tym czy administranor zatwierdził wypożyczenie oraz IsCanceled – flaga z informacją czy administrator odrzucił wypożyczenie.

Funkcjonalności
•	Zarządzanie książkami: Umożliwi dodawanie, edycję, usuwanie i przeglądanie książek w systemie. Książki będą przechowywane w bazie danych wraz z informacjami takimi jak tytuł, autor oraz dostępność.
•	Zarządzanie wypożyczeniami: Umożliwi wypożyczanie książek przez klientów. System będzie kontrolować dostępność książek i generować raporty na temat wypożyczeń. Administranor może zaakceptować lub odrzucić wypożyczenie. Jeżeli wypożyczenie nie zostało jeszcze potwierdzone przez administratora, to użytkownik może odwołać wypożyczenie książki.
•	Rejestracja i logowanie użytkowników: Umożliwi użytkownikom rejestrację nowych kont oraz logowanie na istniejące konta. Mechanizm rejestracji i logowania będzie zabezpieczony przed nieautoryzowanym dostępem.

