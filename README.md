# API Dokumentation f�r `ProductEndpointExtensions`

Detta API hanterar CRUD-operationer (Create, Read, Update, Delete) f�r `Product`-entiteter. Nedan f�ljer en detaljerad beskrivning av tillg�ngliga endpoints, deras parametrar och svar.

---

## **H�mta alla produkter**
- **Metod:** `GET`
- **Endpoint:** `/products`
- **Beskrivning:** H�mtar en lista med alla produkter.
- **Request:**
  - Ingen body beh�vs.
- **Response:**
  - **200 OK:** Returnerar en lista av produkter i JSON-format.
  - **Exempel p� svar:**
    ```json
    [
      {
        "id": "1",
        "name": "Laptop",
        "price": 12000.50
      },
      {
        "id": "2",
        "name": "Smartphone",
        "price": 8999.99
      }
    ]
    ```

---

## **H�mta en produkt**
- **Metod:** `GET`
- **Endpoint:** `/products/{id}`
- **Beskrivning:** H�mtar detaljer om en specifik produkt baserat p� ID.
- **Parametrar:**
  - **Path Parameter:**
    - `id` (string): ID f�r produkten som ska h�mtas.
- **Request:**
  - Ingen body beh�vs.
- **Response:**
  - **200 OK:** Returnerar produkten som JSON.
    ```json
    {
      "id": "1",
      "name": "Laptop",
      "price": 12000.50
    }
    ```
  - **404 Not Found:** Om produkten inte hittas.

---

## **Skapa en ny produkt**
- **Metod:** `POST`
- **Endpoint:** `/products`
- **Beskrivning:** Skapar en ny produkt i systemet.
- **Request:**
  - **Body:** JSON som beskriver produkten som ska skapas.
    ```json
    {
      "name": "Laptop",
      "price": 12000.50
    }
    ```
- **Response:**
  - **201 Created:** Produkten har skapats.
    ```json
    {
      "id": "1",
      "name": "Laptop",
      "price": 12000.50
    }
    ```
  - **400 Bad Request:** Om datan i requesten �r ogiltig.

---

## **Uppdatera en produkt**
- **Metod:** `PUT`
- **Endpoint:** `/products/{id}`
- **Beskrivning:** Uppdaterar en befintlig produkt baserat p� ID.
- **Parametrar:**
  - **Path Parameter:**
    - `id` (string): ID f�r produkten som ska uppdateras.
- **Request:**
  - **Body:** JSON som inneh�ller de nya produktuppgifterna.
    ```json
    {
      "name": "Smartphone",
      "price": 8999.99
    }
    ```
- **Response:**
  - **204 No Content:** Produkten har uppdaterats.
  - **404 Not Found:** Om produkten inte hittas.

---

## **Radera en produkt**
- **Metod:** `DELETE`
- **Endpoint:** `/products/{id}`
- **Beskrivning:** Raderar en produkt baserat p� ID.
- **Parametrar:**
  - **Path Parameter:**
    - `id` (string): ID f�r produkten som ska raderas.
- **Request:**
  - Ingen body beh�vs.
- **Response:**
  - **204 No Content:** Produkten har raderats.
  - **404 Not Found:** Om produkten inte hittas.

---

## **Felhantering**
Alla endpoints kan returnera f�ljande generiska felkoder:
- **400 Bad Request:** N�r ogiltiga data skickas i f�rfr�gan.
- **404 Not Found:** N�r en resurs inte hittas.
- **500 Internal Server Error:** Vid ov�ntade fel i servern.

---

## **Exempel p� cURL-kommandon**

### H�mta alla produkter:
```bash
curl -X GET http://localhost:5000/products
