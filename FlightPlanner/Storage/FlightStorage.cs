using System;
using FlightPlanner.Models;

namespace FlightPlanner.Storage
{
    public class FlightStorage 
    {
        public static Flight ConvertToFlight(AddFlightRequest request)
        {
            var flight = new Flight
            {
                From = request.From,
                To = request.To,
                Carrier = request.Carrier,
                DepartureTime = request.DepartureTime,
                ArrivalTime = request.ArrivalTime
            };

            return flight;
        }

        public static bool RequestValidFlights(SearchFlightsRequest request)
        {
            if (request is null || request.DepartureDate is null ||
                request.From is null || request.To is null ||
                request.From == request.To)
                return false;

            return true;
        }

        public static bool IsValid(AddFlightRequest request)
        {
            if (String.IsNullOrEmpty(request.ArrivalTime) || String.IsNullOrEmpty(request.DepartureTime) ||
                String.IsNullOrEmpty(request.Carrier))
                return false;

            if (request.From is null || request.To is null)
                return false;

            if (String.IsNullOrEmpty(request.From.AirportName) || String.IsNullOrEmpty(request.From.City) ||
                String.IsNullOrEmpty(request.From.Country))
                return false;

            if (String.IsNullOrEmpty(request.To.AirportName) || String.IsNullOrEmpty(request.To.City) ||
                String.IsNullOrEmpty(request.To.Country))
                return false;

            if (request.From.AirportName.ToLower().Trim() == request.To.AirportName.ToLower().Trim() &&
                request.From.City.ToLower().Trim() == request.To.City.ToLower().Trim() &&
                request.From.Country.ToLower().Trim() == request.To.Country.ToLower().Trim())
                return false;

            var arrivalTime = DateTime.Parse(request.ArrivalTime);
            var departureTime = DateTime.Parse(request.DepartureTime);

            if (arrivalTime <= departureTime)
                return false;

            return true;
        }
    }
}