SELECT a."HostId",
	   SUM(b."TotalPrice") AS "TotalRevenue"
FROM "Bookings" b
JOIN "Apartments" a ON a."Id" = b."ApartmentId"
GROUP BY a."HostId";
