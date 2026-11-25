SELECT "Apartments"."HostId",
	   SUM("Bookings"."TotalPrice") AS "TotalRevenue"
FROM "Bookings"
JOIN "Apartments" ON "Apartments"."Id" = "Bookings"."ApartmentId"
GROUP BY "Apartments"."HostId"
HAVING SUM("Bookings"."TotalPrice") > @Threshold;
