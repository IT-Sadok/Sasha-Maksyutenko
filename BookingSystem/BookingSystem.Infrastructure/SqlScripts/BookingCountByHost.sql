SELECT "Apartments"."HostId", COUNT(*) AS "BookingCount"
FROM "Bookings" JOIN "Apartments" ON "Apartments"."Id" = "Bookings"."ApartmentId" GROUP BY "Apartments"."HostId" 
