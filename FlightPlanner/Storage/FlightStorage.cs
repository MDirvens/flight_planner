using System;
using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Models;

namespace FlightPlanner.Storage
{
    public class FlightStorage
    {
        public static List<Flight> _flights = new List<Flight>();
        private static int _id;

        public static Flight AddFlight(AddFlightRequest request)
        {
                var flight = new Flight
                {
                    Id = ++_id,
                    From = request.From,
                    To = request.To,
                    Carrier = request.Carrier,
                    DepartureTime = request.DepartureTime,
                    ArrivalTime = request.ArrivalTime
                };

                _flights.Add(flight);

                return flight;
        }

        public static Flight GetFlight(int id)
        {
            return _flights.SingleOrDefault(f => f.Id == id);
        }

        public static PageResult FindFlights(SearchFlightsRequest request)
        {
            var pageResult = new PageResult
            {
                Items = new List<Flight>(_flights),
                TotalItems = _flights.Count,
                Page = _flights.Count
            };

            return pageResult;
        }

        public static bool RequestValidFlights(SearchFlightsRequest request)
        {
                if (request is null || request.DepartureDate is null ||
                request.From is null || request.To is null ||
                request.From == request.To)
                    return false;

                return true;
        }

        public static void DeleteFlight(int id)
        {
                var flight = GetFlight(id);
                if (flight != null)
                    _flights.Remove(flight);
        }

        public static List<Airport> FindAirports(string airport)
        {
            airport = airport.ToLower().Trim();

            var fromAirport = _flights.Where(f =>
                f.From.AirportName.ToLower().Trim().Contains(airport) ||
                f.From.City.ToLower().Trim().Contains(airport) ||
                f.From.Country.ToLower().Trim().Contains(airport)).
                Select(a => a.From).ToList();

            var toAirport = _flights.Where(f =>
                f.To.AirportName.ToLower().Trim().Contains(airport) ||
                f.To.City.ToLower().Trim().Contains(airport) ||
                f.To.Country.ToLower().Trim().Contains(airport)).
                Select(a => a.To).ToList();

            return fromAirport.Concat(toAirport).ToList();
        }

        public static void ClearFlight()
        {
            _flights.Clear();
            _id = 0;
        }

        public static bool Exist(AddFlightRequest request)
        {
                return _flights.Any(f =>
                    f.Carrier == request.Carrier &&
                    f.DepartureTime == request.DepartureTime &&
                    f.ArrivalTime == request.ArrivalTime &&
                    f.To.AirportName == request.To.AirportName &&
                    f.From.AirportName == request.From.AirportName);
        }

        public static bool IsValid(AddFlightRequest request)
        {
                if (String.IsNullOrEmpty(request.ArrivalTime) || String.IsNullOrEmpty(request.DepartureTime) ||
                    String.IsNullOrEmpty(request.Carrier))
                {
                    Console.WriteLine(request.ArrivalTime);
                    Console.WriteLine(request.Carrier);
                    Console.WriteLine(request.DepartureTime);
                    Console.WriteLine(request.From);
                    Console.WriteLine(request.To);
                    return false;
                }
                    
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