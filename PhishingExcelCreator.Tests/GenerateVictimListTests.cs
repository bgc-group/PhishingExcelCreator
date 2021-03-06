using Xunit;
using Lib.Models;
using System.Collections.Generic;
using System;
using Lib.GenerateVictimList;

namespace PhishingExcelCreator.Tests
{
  public class GenerateVictimListTests
  {
    [Fact]
    public void GenerateList_WithRandomSort_ReturnsRandomList()
    {

      DateTime startTime = new DateTime(
        DateTime.Today.Year,
        DateTime.Today.Month,
        DateTime.Today.Day + 1,
        12,
        0,
        0);

      List<Victim> inputVictims = new List<Victim>();
      inputVictims.Add(new Victim("Aaron Jones", "aaron.jones@csintelligence.asia", "R&D"));
      inputVictims.Add(new Victim("John Doe", "john.doe@csintelligence.asia", "Sales"));
      inputVictims.Add(new Victim("John Meyers", "john.meyers@csintelligence.asia", "Sales"));

      GenerateOptions options = new GenerateOptions();
      options.Randomize = true;
      options.MinutesBetweenScheduledEmails = 5;
      options.StartAtDateTime = startTime;
      options.MinimumHour = 8;
      options.MinimumMinute = 30;
      options.MaximumHour = 18;
      options.MaximumMinute = 0;

      GenerateVictims sut = new GenerateVictims();
      List<GeneratedVictim> actual = sut.GenerateVictimList(inputVictims, options);

      Assert.NotEmpty(actual);
      Assert.Equal(inputVictims.Count, actual.Count);
    }

    [Fact]
    public void GenerateList_WithoutRandomSort_ReturnsSortedList()
    {

      DateTime startTime = new DateTime(
        DateTime.Today.Year,
        DateTime.Today.Month,
        DateTime.Today.Day + 1,
        12,
        0,
        0);

      List<Victim> inputVictims = new List<Victim>();
      inputVictims.Add(new Victim("John Meyers", "john.meyers@csintelligence.asia", "Sales"));
      inputVictims.Add(new Victim("John Doe", "john.doe@csintelligence.asia", "Sales"));
      inputVictims.Add(new Victim("Aaron Jones", "aaron.jones@csintelligence.asia", "R&D"));

      GenerateOptions options = new GenerateOptions();
      options.Randomize = false;
      options.MinutesBetweenScheduledEmails = 5;
      options.StartAtDateTime = startTime;
      options.MinimumHour = 8;
      options.MinimumMinute = 30;
      options.MaximumHour = 18;
      options.MaximumMinute = 0;

      GenerateVictims sut = new GenerateVictims();
      List<GeneratedVictim> actual = sut.GenerateVictimList(inputVictims, options);

      Assert.NotEmpty(actual);
      Assert.Equal(inputVictims.Count, actual.Count);

      Assert.Equal("R&D", actual[0].Dept);
      Assert.Equal("Sales", actual[1].Dept);
      Assert.Equal("Sales", actual[2].Dept);

      Assert.Equal("Aaron Jones", actual[0].Name);
      Assert.Equal("John Doe", actual[1].Name);
      Assert.Equal("John Meyers", actual[2].Name);

      Assert.Equal("aaron.jones@csintelligence.asia", actual[0].Email);
      Assert.Equal("john.doe@csintelligence.asia", actual[1].Email);
      Assert.Equal("john.meyers@csintelligence.asia", actual[2].Email);

      Assert.Equal(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 1, 12, 0, 0), actual[0].SendOutTime);
      Assert.Equal(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 1, 12, 5, 0), actual[1].SendOutTime);
      Assert.Equal(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 1, 12, 10, 0), actual[2].SendOutTime);
    }

    [Fact]
    public void GenerateList_WithoutRandomSort_CreatesSendOutTimesAfterMinimumHour()
    {

      DateTime startTime = new DateTime(
        DateTime.Today.Year,
        DateTime.Today.Month,
        DateTime.Today.Day + 1,
        3,
        0,
        0);

      List<Victim> inputVictims = new List<Victim>();
      inputVictims.Add(new Victim("John Meyers", "john.meyers@csintelligence.asia", "Sales"));
      inputVictims.Add(new Victim("John Doe", "john.doe@csintelligence.asia", "Sales"));
      inputVictims.Add(new Victim("Aaron Jones", "aaron.jones@csintelligence.asia", "R&D"));

      GenerateOptions options = new GenerateOptions();
      options.Randomize = false;
      options.MinutesBetweenScheduledEmails = 5;
      options.StartAtDateTime = startTime;
      options.MinimumHour = 8;
      options.MinimumMinute = 30;
      options.MaximumHour = 18;
      options.MaximumMinute = 0;

      GenerateVictims sut = new GenerateVictims();
      List<GeneratedVictim> actual = sut.GenerateVictimList(inputVictims, options);

      Assert.NotEmpty(actual);
      Assert.Equal(inputVictims.Count, actual.Count);

      Assert.Equal("R&D", actual[0].Dept);
      Assert.Equal("Sales", actual[1].Dept);
      Assert.Equal("Sales", actual[2].Dept);

      Assert.Equal("Aaron Jones", actual[0].Name);
      Assert.Equal("John Doe", actual[1].Name);
      Assert.Equal("John Meyers", actual[2].Name);

      Assert.Equal("aaron.jones@csintelligence.asia", actual[0].Email);
      Assert.Equal("john.doe@csintelligence.asia", actual[1].Email);
      Assert.Equal("john.meyers@csintelligence.asia", actual[2].Email);

      Assert.Equal(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 1, 8, 30, 0), actual[0].SendOutTime);
      Assert.Equal(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 1, 8, 35, 0), actual[1].SendOutTime);
      Assert.Equal(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 1, 8, 40, 0), actual[2].SendOutTime);
    }

    [Fact]
    public void GenerateList_WithoutRandomSort_CreatesSendOutTimesAfterMaximumHour()
    {

      DateTime startTime = new DateTime(
        DateTime.Today.Year,
        DateTime.Today.Month,
        DateTime.Today.Day + 1,
        19,
        30,
        0);

      List<Victim> inputVictims = new List<Victim>();
      inputVictims.Add(new Victim("John Meyers", "john.meyers@csintelligence.asia", "Sales"));
      inputVictims.Add(new Victim("John Doe", "john.doe@csintelligence.asia", "Sales"));
      inputVictims.Add(new Victim("Aaron Jones", "aaron.jones@csintelligence.asia", "R&D"));

      GenerateOptions options = new GenerateOptions();
      options.Randomize = false;
      options.MinutesBetweenScheduledEmails = 5;
      options.StartAtDateTime = startTime;
      options.MinimumHour = 8;
      options.MinimumMinute = 30;
      options.MaximumHour = 18;
      options.MaximumMinute = 0;

      GenerateVictims sut = new GenerateVictims();
      List<GeneratedVictim> actual = sut.GenerateVictimList(inputVictims, options);

      Assert.NotEmpty(actual);
      Assert.Equal(inputVictims.Count, actual.Count);

      Assert.Equal("R&D", actual[0].Dept);
      Assert.Equal("Sales", actual[1].Dept);
      Assert.Equal("Sales", actual[2].Dept);

      Assert.Equal("Aaron Jones", actual[0].Name);
      Assert.Equal("John Doe", actual[1].Name);
      Assert.Equal("John Meyers", actual[2].Name);

      Assert.Equal("aaron.jones@csintelligence.asia", actual[0].Email);
      Assert.Equal("john.doe@csintelligence.asia", actual[1].Email);
      Assert.Equal("john.meyers@csintelligence.asia", actual[2].Email);

      Assert.Equal(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 2, 8, 30, 0), actual[0].SendOutTime);
      Assert.Equal(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 2, 8, 35, 0), actual[1].SendOutTime);
      Assert.Equal(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 2, 8, 40, 0), actual[2].SendOutTime);
    }

    [Fact]
    public void GenerateList_WithRandomSort_CreatesSendOutTimesAfterMinimumMHour()
    {

      DateTime startTime = new DateTime(
        DateTime.Today.Year,
        DateTime.Today.Month,
        DateTime.Today.Day + 1,
        3,
        0,
        0);

      List<Victim> inputVictims = new List<Victim>();
      inputVictims.Add(new Victim("John Meyers", "john.meyers@csintelligence.asia", "Sales"));
      inputVictims.Add(new Victim("John Doe", "john.doe@csintelligence.asia", "Sales"));
      inputVictims.Add(new Victim("Aaron Jones", "aaron.jones@csintelligence.asia", "R&D"));

      GenerateOptions options = new GenerateOptions();
      options.Randomize = true;
      options.MinutesBetweenScheduledEmails = 5;
      options.StartAtDateTime = startTime;
      options.MinimumHour = 8;
      options.MinimumMinute = 30;
      options.MaximumHour = 18;
      options.MaximumMinute = 0;

      GenerateVictims sut = new GenerateVictims();
      List<GeneratedVictim> actual = sut.GenerateVictimList(inputVictims, options);

      Assert.NotEmpty(actual);
      Assert.Equal(inputVictims.Count, actual.Count);

      Assert.Equal(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 1, 8, 30, 0), actual[0].SendOutTime);
      Assert.Equal(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 1, 8, 35, 0), actual[1].SendOutTime);
      Assert.Equal(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 1, 8, 40, 0), actual[2].SendOutTime);
    }

    [Fact]
    public void GenerateList_WithRandomSort_CreatesSendOutTimesAfterMaximumHour()
    {

      DateTime startTime = new DateTime(
        DateTime.Today.Year,
        DateTime.Today.Month,
        DateTime.Today.Day + 1,
        19,
        30,
        0);

      List<Victim> inputVictims = new List<Victim>();
      inputVictims.Add(new Victim("John Meyers", "john.meyers@csintelligence.asia", "Sales"));
      inputVictims.Add(new Victim("John Doe", "john.doe@csintelligence.asia", "Sales"));
      inputVictims.Add(new Victim("Aaron Jones", "aaron.jones@csintelligence.asia", "R&D"));

      GenerateOptions options = new GenerateOptions();
      options.Randomize = true;
      options.MinutesBetweenScheduledEmails = 5;
      options.StartAtDateTime = startTime;
      options.MinimumHour = 8;
      options.MinimumMinute = 30;
      options.MaximumHour = 18;
      options.MaximumMinute = 0;

      GenerateVictims sut = new GenerateVictims();
      List<GeneratedVictim> actual = sut.GenerateVictimList(inputVictims, options);

      Assert.NotEmpty(actual);
      Assert.Equal(inputVictims.Count, actual.Count);

      Assert.Equal(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 2, 8, 30, 0), actual[0].SendOutTime);
      Assert.Equal(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 2, 8, 35, 0), actual[1].SendOutTime);
      Assert.Equal(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 2, 8, 40, 0), actual[2].SendOutTime);
    }
  }
}