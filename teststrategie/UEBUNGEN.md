# Übungen

## Übung 1

Wir haben folgende Beschreibung einer Verkaufssoftware:

*Über die Verkaufssoftware kann das Autohaus seinen Verkäufern Rabattregeln vorgeben: Bei einem Kaufpreis von weniger
als 15'000 CHF soll kein Rabatt gewährt werden. Bei einem Preis bis zu 20'000 CHF sind 5% Rabatt angemessen. Liegt der
Kaufpreis unter 25'000 CHF sind 7% Rabatt möglich, darüber sind 8,5 % Rabatt zu gewähren.*

### Aufgabe

Leiten Sie aus dieser Beschreibung Testfälle ab. Wir wollen beide Varianten von Testfällen untersuchen.

* Eine Tabelle mit abstrakten Testfällen. Hier verwenden Sie logische Operatoren wie > , < , etc.

| ID | Input | Output |
| -------- | ------- | -------- |
| ID    | Kaufpreis < 15'000 | 0% Rabatt |
| ID    | Kaufpreis > 15'000 && Kaufpreis < 20'000 | 5% Rabatt |
| ID    | Kaufpreis > 20'000 && Kaufpreis < 25'000 | 7% Rabatt |
| ID    | Kaufpreis > 25'000 | 8.5% Rabatt |

* Eine Tabelle mit konkreten Testfällen. Hier verwenden Sie ganz konkrete Eingabe-Werte, um die Testfälle zu erstellen.

| ID | Input | Output |
| -------- | ------- | -------- |
| ID    | 14'000 | 0% Rabatt |
| ID    | 18'000 | 5% Rabatt |
| ID    | 20'500 | 7% Rabatt |
| ID    | 24'800 | 7% Rabatt |
| ID    | 30'000 | 8.5% Rabatt |
---

## Übung 2

Suchen Sie sich eine Webseite zum Thema **Autovermietung**. -> www.europcar.ch

Definieren Sie *funktionale Black-Box Tests*, die Sie brauchen, um diese Plattform zu betreiben. <br/>
*Listen Sie die 5 wichtigsten Testfälle auf*

Erstellen Sie eine Tabelle mit diesen Testfälle als Markdown und stellen Sie diese in Ihr Repository.

| ID | Beschreibung (Input & Output) |
|----|--------------|
| 1 | Programm startet korrekt | Nach dem Klicken auf die Website-URL wird die Startseite angezeigt |
| 2 | Suchfunktion funktioniert richtig | Nachdem Sie das Eingabefeld ausgefüllt und auf „Suchen“ geklickt haben, werden die Verfügbare Fahrzeuge angezeigt |
| 3 | Die Weltkarte funktioniert einwandfrei. | Weltkarte zeigt nächstgelegene Abholorte an |
| 4 | Support funkioniert korrekt | Bei Problemen können Sie sich schnell an den Support wenden |
| 5 | Login funktioniert richtig | Ich kann mich anmelden, wenn ich den richtigen Benutzernamen und das richtige Passwort eingebe. |

---

## Übung 3

Sie haben folgende Software einer simplen Bank-Software. Laden Sie das Source-Zip herunter und erstellen Sie ein lokales
Projekt in Ihrer IDE. Achtung! Sie müssen auch die JAR-Files für GSON und OKHTTP installieren. Alternativ können Sie das
[Maven Projekt](https://gitlab.com/ch-tbz-it/Stud/m450/m450/-/blob/main/Unterlagen/teststrategie/bank-software-mvn.zip) verwenden,
um es ohne die JAR-Files in Betrieb zu nehmen. Die Software plus JAR-Files finden Sie
hier: https://gitlab.com/ch-tbz-it/Stud/m450/m450/-/tree/main/Unterlagen/teststrategie
Machen Sie sich mit dem Code vertraut.

Wir wollen ganz grob herausfinden, was für Testfälle es in dieser Software gibt.

* Identifizieren Sie mögliche Black-Box Testfälle, welche Sie als Benutzer testen können.
| ID | Beschreibung |
|----|--------------|
| 1 | cannot send money to another account if my account doesn't have any deposit |
| 2 | cannot take out more cash than available in account |
| 3 | currency exchange work correctly when depositing cash into a different currency account |
| 4 | cannot select an unexisting account (z.B. account 7 was removed, now i can't access it anymore) |
| 5 | using a currency other than USD, CHF or EUR when creating an account |

* Welche Methoden im Code könnten für White-Box Testfälle verwendet werden?
- technically you could/should test every function/method written.
* Was würden Sie am Code generell verbessern, welche Best Practices fallen Ihnen ein
- the functions/methods `chooseAccount` and `editAccount` look very similar, since they handle user input, i personally
would try to simplify it by creating a function for repeating stuff.
- remove unused stuff like the `pseudoDeleteAccount` function/method.
- add the currency enum in the main file in a separate file like `currency.enum.java` or `ECurrency.java`.
Listen Sie Ihre Testfälle tabellarisch auf in einem Markdown-Dokument und stellen Sie Ihre Lösung in Ihr Repository.