using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Api.Track.co.Data;
using Web.Api.Track.co.DTOs;
using Web.Api.Track.co.DTOs.Contract;
using Web.Api.Track.co.DTOs.NPS;
using Web.Api.Track.co.DTOs.Widget;

namespace Web.Api.Track.co.Service
{
    public class WidgetService
    {
        private readonly ILogger<WidgetLogger> _logger;
        private readonly AppDbContext _context;

        public WidgetService(ILogger<WidgetLogger> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public string GenerateHTML(Widget widget)
        {
            string question = widget.Question;
            int widgetId = widget.Id;

            Console.WriteLine("Generating HTML for widget with ID " + widgetId + " and question: " + question);

            var html =
                $@"<style>:root {{--accent-color: rgb(0, 123, 255);--accent-color-hover: rgb(0, 86, 179);}}.widget-container {{font-family: Arial, sans-serif;background-color: #f0f0f0;padding: 20px;border-radius: 8px;box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);width: 300px;margin: auto;}}.widget-title {{color: #333;font-size: 18px;margin-bottom: 15px;}}.widget-label {{font-size: 14px;color: #555;display: block;margin-bottom: 5px;}}.widget-input,    .widget-select {{width: 100%;padding: 10px;margin-bottom: 20px;border: 1px solid #ddd;border-radius: 4px;box-sizing: border-box;}}.widget-button {{background-color: rgb(0, 123, 255);color: white;padding: 10px 20px;border: none;border-radius: 4px;cursor: pointer;font-size: 16px;}}.widget-button:hover {{background-color: rgb(0, 86, 179);}}</style>
                                <form id='widgetForm' class='widget-container'><p class='widget-title'>Welcome to the widget</p><div><label class='widget-label'>{question}</label></div><div><label class='widget-label'>Answer:</label><input class='widget-input' name='answer' placeholder='Your answer'optional></div><div><label class='widget-label' for='rating'>Rating (1-10):</label><select class='widget-select' name='rating'required><option value=''>Choose a rating</option><option value='1'>1</option><option value='2'>2</option><option value='3'>3</option><option value='4'>4</option><option value='5'>5</option><option value='6'>6</option><option value='7'>7</option><option value='8'>8</option><option value='9'>9</option><option value='10'>10</option></select></div><button type='submit' class='widget-button'>Send</button></form>
                                <script>document.getElementById('widgetForm').addEventListener('submit', function (e) {{ e.preventDefault(); const formData = new FormData(e.target); const widgetId = {widgetId}; const data = {{ widgetId, answer: formData.get('answer') || '', rating: parseInt(formData.get('rating'), 10) }}; fetch(`http://localhost:5244/widgets/{widgetId}/nps`, {{ method: 'POST', headers: {{ 'Content-Type': 'application/json', }}, body: JSON.stringify(data), }}).then(response => response.json()).then(data => {{ console.log('Success:', data); alert('Form submitted successfully!'); }}).catch((error) => {{ console.error('Error:', error); alert('An error occurred while submitting the form.'); }}); }});</script>";

            return html;
        }

        public List<WidgetResponse> GetWidgets()
        {
            try
            {
                _logger.LogInformation("Getting all widgets");
                // var widgets = _context.Widgets.ToList();
                // return everything but the html
                var widgets = _context.Widgets
                    .Select(w => new WidgetResponse
                    {
                        Id = w.Id,
                        Title = w.Title,
                        Link = w.Link,
                        Question = w.Question,
                        Color = w.Color,
                        Nps = w.Nps
                    })
                    .ToList();
                return widgets;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting widgets");
                throw;
            }
        }

        public Widget GetWidgetById(int id)
        {
            try
            {
                _logger.LogInformation($"Getting widget with ID {id} with corresponding NPS records");
                var widget = _context.Widgets
                    .Include(w => w.Nps)
                    .FirstOrDefault(w => w.Id == id);

                if (widget == null)
                {
                    throw new Exception("Widget not found");
                }

                return widget;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting widgets");
                throw;
            }
        }

        public string GetEmbeddedHtml(int id)
        {
            try
            {
                var widget = _context.Widgets
                    .Where(w => w.Id == id)
                    .Select(w => w.Html)
                    .FirstOrDefault();

                if (widget == null)
                {
                    throw new Exception("Widget not found");
                }

                return widget;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting embedded HTML");
                throw;
            }
        }

        public Widget CreateWidget(WidgetCreateDto widgetDto)
        {
            try
            {
                widgetDto.Validate();

                if (!widgetDto.IsValid)
                {
                    throw new Exception(widgetDto.Notifications.ToString());
                }

                _logger.LogInformation("Creating a new widget");

                var widget = new Widget
                {
                    Title = widgetDto.Title,
                    Link = widgetDto.Link,
                    Question = widgetDto.Question,
                    Color = "#e5e7eb"
                };

                var html = GenerateHTML(widget);
                widget.Html = html;

                _context.Widgets.Add(widget);
                _context.SaveChanges();

                return widget;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Nps CreateNps(int widgetId, NPSCreateDto npsDto)
        {
            if (npsDto == null || !npsDto.IsValid)
            {
                _logger.LogError($"Invalid NPS data for widget ID {widgetId}");
                foreach (var notification in npsDto.Notifications)
                {
                    _logger.LogError($"Validation error: {notification.Message}");
                }
                return null;
            }

            try
            {
                _logger.LogInformation($"Creating a new NPS for widget ID {widgetId}");

                var nps = new Nps
                {
                    WidgetId = widgetId,
                    Answer = npsDto.Answer,
                    Rating = npsDto.Rating
                };

                _context.Nps.Add(nps);
                _context.SaveChanges();

                return nps;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error occurred while creating a new NPS for widget ID {widgetId}");
                throw;
            }
        }
        
        public Nps GetNpsById(int widgetId, int npsId)
        {
            try
            {
                _logger.LogInformation($"Getting NPS record with ID {npsId} for widget ID {widgetId}");
                var npsRecord = _context.Nps
                    .Where(n => n.WidgetId == widgetId && n.Id == npsId)
                    .FirstOrDefault();

                if (npsRecord == null)
                {
                    throw new Exception("NPS record not found");
                }

                return npsRecord;
            }
            catch (Exception e)
            {
                _logger.LogError(e,
                    $"Error occurred while fetching NPS record with ID {npsId} for widget ID {widgetId}");
                throw;
            }
        }

        public Widget UpdateWidget(int id, WidgetUpdateDto widgetUpdateDto)
        {
            try
            {
                widgetUpdateDto.Validate();

                _logger.LogInformation($"Updating widget with ID {id}");
                var widget = _context.Widgets.Find(id);
                if (widget == null)
                {
                    throw new Exception("Widget not found");
                }

                widget.Title = widgetUpdateDto.Title ?? widget.Title;
                widget.Link = widgetUpdateDto.Link ?? widget.Link;
                widget.Question = widgetUpdateDto.Question ?? widget.Question;
                widget.Color = widgetUpdateDto.Color ?? widget.Color;

                var newHtml = GenerateHTML(widget);
                widget.Html = newHtml;

                _context.SaveChanges();

                return widget;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error occurred while updating widget with ID {id}");
                throw;
            }
        }

        public Widget DeleteWidget(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting widget with ID {id}");
                var widget = _context.Widgets
                    .Include(w => w.Nps)
                    .FirstOrDefault(w => w.Id == id);

                if (widget == null)
                {
                    throw new Exception("Widget not found");
                }

                foreach (var nps in widget.Nps)
                {
                    _context.Nps.Remove(nps);
                }

                _context.Widgets.Remove(widget);
                _context.SaveChanges();

                return widget;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error occurred while deleting widget with ID {id}");
                throw;
            }
        }
    }
}