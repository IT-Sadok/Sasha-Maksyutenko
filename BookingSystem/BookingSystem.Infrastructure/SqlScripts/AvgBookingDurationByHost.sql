SELECT "Apartments"."HostId",
       AVG(EXTRACT(EPOCH FROM ("Bookings"."EndDate" - "Bookings"."StartDate")) / 86400) AS "AvgDurationDays"
FROM "Bookings"
JOIN "Apartments" ON "Apartments"."Id" = "Bookings"."ApartmentId"
GROUP BY "Apartments"."HostId";