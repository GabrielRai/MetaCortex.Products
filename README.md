# API Dokumentation för `ProductEndpointExtensions`

Detta API hanterar CRUD-operationer (Create, Read, Update, Delete) för `Product`-entiteter. Nedan följer en detaljerad beskrivning av tillgängliga endpoints, deras parametrar och svar.

---

## **Hämta alla produkter**
- **Metod:** `GET`
- **Endpoint:** `/products`
- **Beskrivning:** Hämtar en lista med alla produkter.
- **Request:**
  - Ingen body behövs.
- **Response:**
  - **200 OK:** Returnerar en lista av produkter i JSON-format.
  - **Exempel på svar:**
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

## **Hämta en produkt**
- **Metod:** `GET`
- **Endpoint:** `/products/{id}`
- **Beskrivning:** Hämtar detaljer om en specifik produkt baserat på ID.
- **Parametrar:**
  - **Path Parameter:**
    - `id` (string): ID för produkten som ska hämtas.
- **Request:**
  - Ingen body behövs.
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
  - **400 Bad Request:** Om datan i requesten är ogiltig.

---

## **Uppdatera en produkt**
- **Metod:** `PUT`
- **Endpoint:** `/products/{id}`
- **Beskrivning:** Uppdaterar en befintlig produkt baserat på ID.
- **Parametrar:**
  - **Path Parameter:**
    - `id` (string): ID för produkten som ska uppdateras.
- **Request:**
  - **Body:** JSON som innehåller de nya produktuppgifterna.
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
- **Beskrivning:** Raderar en produkt baserat på ID.
- **Parametrar:**
  - **Path Parameter:**
    - `id` (string): ID för produkten som ska raderas.
- **Request:**
  - Ingen body behövs.
- **Response:**
  - **204 No Content:** Produkten har raderats.
  - **404 Not Found:** Om produkten inte hittas.

---

## **Felhantering**
Alla endpoints kan returnera följande generiska felkoder:
- **400 Bad Request:** När ogiltiga data skickas i förfrågan.
- **404 Not Found:** När en resurs inte hittas.
- **500 Internal Server Error:** Vid oväntade fel i servern.

---

## **Exempel på cURL-kommandon**

### Hämta alla produkter:
```bash
curl -X GET http://localhost:5000/products
