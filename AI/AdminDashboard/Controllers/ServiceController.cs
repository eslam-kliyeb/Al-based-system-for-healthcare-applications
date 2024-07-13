using AdminDashboard.Models;
using AI.Core.DTOs;
using AI.Core.Entities;
using AI.Core.Interfaces.Service;
using AI.Core.Specification;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace AdminDashboard.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IPatientService _petientService;
        private readonly IChatBotService _chatBotService;
        private readonly ISegmentationService _segmentationService;
        private readonly IClassifierService _classifierService;
        private readonly IDoctorService _doctorService;
        SpecificationParameters SpecificationParameters=new SpecificationParameters
        {
            PageIndex = 1,
            PageSize = 5,
            Search = null,
            Sort = null,
        };
        public ServiceController(IPatientService patientService, 
                                 IChatBotService chatBotService , 
                                 ISegmentationService segmentationService , 
                                 IClassifierService classifierService,
                                 IDoctorService doctorService)
        {
            _petientService = patientService;
            _chatBotService = chatBotService;
            _segmentationService = segmentationService;
            _classifierService = classifierService;
            _doctorService = doctorService;

        }
        public IActionResult Index(LoginDto loginDto)
        {
			return View(loginDto);
        }
        public async Task<IActionResult> Patient(LoginDto loginDto)
        {
            var res = await _petientService.GetAllPatientsAsync(loginDto.Email, SpecificationParameters);
            var patient = res.Data;
			var viewModel = new PatientViewModel
			{
				LoginDto = loginDto,
				Patient = patient
			};

			return View(viewModel);
        }
        public IActionResult Create(LoginDto loginDto)
        {
			var viewModel = new CreateViewModel
			{
				loginDto = loginDto,
                patientInputDto = new PatientInputDto()
			};
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {
                int x = await _petientService.AddPatientAsync(createViewModel.loginDto.Email, createViewModel.patientInputDto);
                return RedirectToAction("Index", createViewModel.loginDto);
            }
            return View(createViewModel);
        }
		public async Task<IActionResult> Details(LoginDto loginDto)
        {
			
            int x=int.Parse(loginDto.Password);
			var res = await _petientService.GetPatientAsync(loginDto.Email, x);
            var viewModel = new DetailsViewModel
            {
                loginDto = loginDto,
                patientToReturnDto = res
            };
             return View(viewModel);
		}
        public async Task<IActionResult> Delete(LoginDto loginDto,int y=0)
        {

            int x = int.Parse(loginDto.Password);
            var res = await _petientService.GetPatientAsync(loginDto.Email, x);
            var viewModel = new DetailsViewModel
            {
                loginDto = loginDto,
                patientToReturnDto = res
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                int x = int.Parse(loginDto.Password);
                x = await _petientService.DeletePatient(loginDto.Email,x);
                return RedirectToAction("Index", loginDto);
            }
            return RedirectToAction("Index", loginDto);
        }

        public IActionResult ChatBot(string email, string password)
        {
            var loginDto = new LoginDto
            {
                Email = email,
                Password = password
            };
            var ViewModel = new ChatBotViewModel
            {
                loginDto=loginDto,
                message = " ",
                answer = "Answer ChatBot"

            };
            return View(ViewModel);
        }
        public IActionResult ChatBotView(string email, string password, string message, string answer)
        {

            var loginDto = new LoginDto
            {
                Email = email,
                Password = password
            };

            var ViewModel = new ChatBotViewModel
            {
                loginDto = loginDto,
                message = message,
                answer = answer
            };
            return View(ViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ChatBot(ChatBotViewModel chatBotViewModel)
        {
            if (ModelState.IsValid)
            {
                chatBotViewModel.answer = await _chatBotService.ChatBot(chatBotViewModel.message);
                return RedirectToAction("ChatBotView", new
                {
                    email = chatBotViewModel.loginDto.Email,
                    message = chatBotViewModel.message,
                    password = chatBotViewModel.loginDto.Password,
                    answer = chatBotViewModel.answer
                });
            }
            return View(chatBotViewModel);
        }
        public IActionResult Segmentation(string email, string password)
        {

            var loginDto = new LoginDto
            {
                Email = email,
                Password = password
            };

            var ViewModel = new SegmentationViewModel
            {
                loginDto = loginDto,
                ImageName = "NO"
            };
            return View(ViewModel);
        }
        public  IActionResult SegmentationView(string email, string password, string imageName)
        {
            var loginDto = new LoginDto
            {
                Email = email,
                Password = password
            };

            var ViewModel = new SegmentationViewModel
            {
                loginDto = loginDto,
                ImageName = imageName
            };
            return View(ViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Segmentation(SegmentationViewModel segmentationViewModel)
        {
            if (ModelState.IsValid)
            {
                segmentationViewModel.ImageName = await _segmentationService.Segmentation(segmentationViewModel.Image);
                return RedirectToAction("SegmentationView", new
                {
                    email = segmentationViewModel.loginDto.Email,
                    password = segmentationViewModel.loginDto.Password,
                    imageName = segmentationViewModel.ImageName
            });
            }
            return View(segmentationViewModel);
        }
        public IActionResult Classification(string email, string password)
        {
            var loginDto = new LoginDto
            {
                Email = email,
                Password = password
            };
            var ViewModel = new ClassificationViewModel
            {
                loginDto = loginDto,
                answer = "NO"

            };
            return View(ViewModel);
        }
        public IActionResult ClassificationView(string email, string password, IFormFile Image, string answer)
        {
            var loginDto = new LoginDto
            {
                Email = email,
                Password = password
            };
            var ViewModel = new ClassificationViewModel
            {
                loginDto = loginDto,
                Image = Image,
                answer = answer
            };
            return View(ViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Classification(ClassificationViewModel classificationViewModel)
        {
            if (ModelState.IsValid)
            {
                classificationViewModel.answer = await _classifierService.Classifier(classificationViewModel.Image);
                return RedirectToAction("ClassificationView", new
                {
                    email= classificationViewModel.loginDto.Email,
                    password= classificationViewModel.loginDto.Password,
                    Image= classificationViewModel.Image,
                    answer= classificationViewModel.answer

                });
            }
            return View(classificationViewModel);
        }
        public async Task<IActionResult> MyAccount(LoginDto loginDto)
        {
            
            var res = await _doctorService.GetDoctorAsync(loginDto.Email);
            var viewModel = new MyAccountViewModel
            {
                loginDto = loginDto,
                doctorDto = res
            };
            return View(viewModel);
        }
    }
}
