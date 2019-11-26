using MyCompanyName.AbpZeroTemplate.Authorization.Users;
using MyCompanyName.AbpZeroTemplate.Class;
using MyCompanyName.AbpZeroTemplate.Place;
using MyCompanyName.AbpZeroTemplate.Rank;
using MyCompanyName.AbpZeroTemplate.Status;
using MyCompanyName.AbpZeroTemplate.Subject;
using MyCompanyName.AbpZeroTemplate.SubjectEducation;
using MyCompanyName.AbpZeroTemplate.TypeEbook;
using MyCompanyName.AbpZeroTemplate.TypeFile;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.Ebook.Exporting;
using MyCompanyName.AbpZeroTemplate.Ebook.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MyCompanyName.AbpZeroTemplate.Ebook
{
	[AbpAuthorize(AppPermissions.Pages_PbEbooks)]
    public class PbEbooksAppService : AbpZeroTemplateAppServiceBase, IPbEbooksAppService
    {
		 private readonly IRepository<PbEbook> _pbEbookRepository;
		 private readonly IPbEbooksExcelExporter _pbEbooksExcelExporter;
		 private readonly IRepository<User,long> _lookup_userRepository;
		 private readonly IRepository<PbClass,int> _lookup_pbClassRepository;
		 private readonly IRepository<PbPlace,int> _lookup_pbPlaceRepository;
		 private readonly IRepository<PbRank,int> _lookup_pbRankRepository;
		 private readonly IRepository<PbStatus,int> _lookup_pbStatusRepository;
		 private readonly IRepository<PbSubject,int> _lookup_pbSubjectRepository;
		 private readonly IRepository<PbSubjectEducation,int> _lookup_pbSubjectEducationRepository;
		 private readonly IRepository<PbTypeEbook,int> _lookup_pbTypeEbookRepository;
		 private readonly IRepository<PbTypeFile,int> _lookup_pbTypeFileRepository;
		 

		  public PbEbooksAppService(IRepository<PbEbook> pbEbookRepository, IPbEbooksExcelExporter pbEbooksExcelExporter , IRepository<User, long> lookup_userRepository, IRepository<PbClass, int> lookup_pbClassRepository, IRepository<PbPlace, int> lookup_pbPlaceRepository, IRepository<PbRank, int> lookup_pbRankRepository, IRepository<PbStatus, int> lookup_pbStatusRepository, IRepository<PbSubject, int> lookup_pbSubjectRepository, IRepository<PbSubjectEducation, int> lookup_pbSubjectEducationRepository, IRepository<PbTypeEbook, int> lookup_pbTypeEbookRepository, IRepository<PbTypeFile, int> lookup_pbTypeFileRepository) 
		  {
			_pbEbookRepository = pbEbookRepository;
			_pbEbooksExcelExporter = pbEbooksExcelExporter;
			_lookup_userRepository = lookup_userRepository;
		_lookup_pbClassRepository = lookup_pbClassRepository;
		_lookup_pbPlaceRepository = lookup_pbPlaceRepository;
		_lookup_pbRankRepository = lookup_pbRankRepository;
		_lookup_pbStatusRepository = lookup_pbStatusRepository;
		_lookup_pbSubjectRepository = lookup_pbSubjectRepository;
		_lookup_pbSubjectEducationRepository = lookup_pbSubjectEducationRepository;
		_lookup_pbTypeEbookRepository = lookup_pbTypeEbookRepository;
		_lookup_pbTypeFileRepository = lookup_pbTypeFileRepository;
		
		  }

		 public async Task<PagedResultDto<GetPbEbookForViewDto>> GetAll(GetAllPbEbooksInput input)
         {
			
			var filteredPbEbooks = _pbEbookRepository.GetAll()
						.Include( e => e.UserFk)
						.Include( e => e.PbClassFk)
						.Include( e => e.PbPlaceFk)
						.Include( e => e.PbRankFk)
						.Include( e => e.PbStatusFk)
						.Include( e => e.PbSubjectFk)
						.Include( e => e.PbSubjectEducationFk)
						.Include( e => e.PbTypeEbookFk)
						.Include( e => e.PbTypeFileFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.EbookName.Contains(input.Filter) || e.Link.Contains(input.Filter) || e.Discription.Contains(input.Filter) || e.EbookCover.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.EbookNameFilter),  e => e.EbookName.ToLower() == input.EbookNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.LinkFilter),  e => e.Link.ToLower() == input.LinkFilter.ToLower().Trim())
						.WhereIf(input.MinEbookDateStartFilter != null, e => e.EbookDateStart >= input.MinEbookDateStartFilter)
						.WhereIf(input.MaxEbookDateStartFilter != null, e => e.EbookDateStart <= input.MaxEbookDateStartFilter)
						.WhereIf(input.ProFilter > -1,  e => Convert.ToInt32(e.Pro) == input.ProFilter )
						.WhereIf(input.MinEbookPriceFilter != null, e => e.EbookPrice >= input.MinEbookPriceFilter)
						.WhereIf(input.MaxEbookPriceFilter != null, e => e.EbookPrice <= input.MaxEbookPriceFilter)
						.WhereIf(input.MinEbookViewFilter != null, e => e.EbookView >= input.MinEbookViewFilter)
						.WhereIf(input.MaxEbookViewFilter != null, e => e.EbookView <= input.MaxEbookViewFilter)
						.WhereIf(input.MinEbookLikeFilter != null, e => e.EbookLike >= input.MinEbookLikeFilter)
						.WhereIf(input.MaxEbookLikeFilter != null, e => e.EbookLike <= input.MaxEbookLikeFilter)
						.WhereIf(input.MinEbookDislikeFilter != null, e => e.EbookDislike >= input.MinEbookDislikeFilter)
						.WhereIf(input.MaxEbookDislikeFilter != null, e => e.EbookDislike <= input.MaxEbookDislikeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiscriptionFilter),  e => e.Discription.ToLower() == input.DiscriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.EbookCoverFilter),  e => e.EbookCover.ToLower() == input.EbookCoverFilter.ToLower().Trim())
						.WhereIf(input.MinBookPageFilter != null, e => e.BookPage >= input.MinBookPageFilter)
						.WhereIf(input.MaxBookPageFilter != null, e => e.BookPage <= input.MaxBookPageFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.UserFk != null && e.UserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbClassClassNameFilter), e => e.PbClassFk != null && e.PbClassFk.ClassName.ToLower() == input.PbClassClassNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbPlacePlaceNameFilter), e => e.PbPlaceFk != null && e.PbPlaceFk.PlaceName.ToLower() == input.PbPlacePlaceNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbRankRankNameFilter), e => e.PbRankFk != null && e.PbRankFk.RankName.ToLower() == input.PbRankRankNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbStatusStatusNameFilter), e => e.PbStatusFk != null && e.PbStatusFk.StatusName.ToLower() == input.PbStatusStatusNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbSubjectSectionNameFilter), e => e.PbSubjectFk != null && e.PbSubjectFk.SectionName.ToLower() == input.PbSubjectSectionNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbSubjectEducationSubjectNameFilter), e => e.PbSubjectEducationFk != null && e.PbSubjectEducationFk.SubjectName.ToLower() == input.PbSubjectEducationSubjectNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbTypeEbookTypeNameFilter), e => e.PbTypeEbookFk != null && e.PbTypeEbookFk.TypeName.ToLower() == input.PbTypeEbookTypeNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbTypeFileTypeFileNameFilter), e => e.PbTypeFileFk != null && e.PbTypeFileFk.TypeFileName.ToLower() == input.PbTypeFileTypeFileNameFilter.ToLower().Trim());

			var pagedAndFilteredPbEbooks = filteredPbEbooks
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pbEbooks = from o in pagedAndFilteredPbEbooks
                         join o1 in _lookup_userRepository.GetAll() on o.UserId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_pbClassRepository.GetAll() on o.PbClassId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         join o3 in _lookup_pbPlaceRepository.GetAll() on o.PbPlaceId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()
                         
                         join o4 in _lookup_pbRankRepository.GetAll() on o.PbRankId equals o4.Id into j4
                         from s4 in j4.DefaultIfEmpty()
                         
                         join o5 in _lookup_pbStatusRepository.GetAll() on o.PbStatusId equals o5.Id into j5
                         from s5 in j5.DefaultIfEmpty()
                         
                         join o6 in _lookup_pbSubjectRepository.GetAll() on o.PbSubjectId equals o6.Id into j6
                         from s6 in j6.DefaultIfEmpty()
                         
                         join o7 in _lookup_pbSubjectEducationRepository.GetAll() on o.PbSubjectEducationId equals o7.Id into j7
                         from s7 in j7.DefaultIfEmpty()
                         
                         join o8 in _lookup_pbTypeEbookRepository.GetAll() on o.PbTypeEbookId equals o8.Id into j8
                         from s8 in j8.DefaultIfEmpty()
                         
                         join o9 in _lookup_pbTypeFileRepository.GetAll() on o.PbTypeFileId equals o9.Id into j9
                         from s9 in j9.DefaultIfEmpty()
                         
                         select new GetPbEbookForViewDto() {
							PbEbook = new PbEbookDto
							{
                                EbookName = o.EbookName,
                                Link = o.Link,
                                EbookDateStart = o.EbookDateStart,
                                Pro = o.Pro,
                                EbookPrice = o.EbookPrice,
                                EbookView = o.EbookView,
                                EbookLike = o.EbookLike,
                                EbookDislike = o.EbookDislike,
                                Discription = o.Discription,
                                EbookCover = o.EbookCover,
                                BookPage = o.BookPage,
                                Id = o.Id
							},
                         	UserName = s1 == null ? "" : s1.Name.ToString(),
                         	PbClassClassName = s2 == null ? "" : s2.ClassName.ToString(),
                         	PbPlacePlaceName = s3 == null ? "" : s3.PlaceName.ToString(),
                         	PbRankRankName = s4 == null ? "" : s4.RankName.ToString(),
                         	PbStatusStatusName = s5 == null ? "" : s5.StatusName.ToString(),
                         	PbSubjectSectionName = s6 == null ? "" : s6.SectionName.ToString(),
                         	PbSubjectEducationSubjectName = s7 == null ? "" : s7.SubjectName.ToString(),
                         	PbTypeEbookTypeName = s8 == null ? "" : s8.TypeName.ToString(),
                         	PbTypeFileTypeFileName = s9 == null ? "" : s9.TypeFileName.ToString()
						};

            var totalCount = await filteredPbEbooks.CountAsync();

            return new PagedResultDto<GetPbEbookForViewDto>(
                totalCount,
                await pbEbooks.ToListAsync()
            );
         }
        public async Task<PagedResultDto<GetPbEbookForDescription>> GetDesciption(GetAll2PbEbooksInput input)
        {

            var filteredPbEbooks = _pbEbookRepository.GetAll()
                        .Include(e => e.UserFk)
                        .Include(e => e.PbClassFk)
                        .Include(e => e.PbPlaceFk)
                        .Include(e => e.PbRankFk)
                        .Include(e => e.PbStatusFk)
                        .Include(e => e.PbSubjectFk)
                        .Include(e => e.PbSubjectEducationFk)
                        .Include(e => e.PbTypeEbookFk)
                        .Include(e => e.PbTypeFileFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.EbookName.Contains(input.Filter) || e.Link.Contains(input.Filter) || e.Discription.Contains(input.Filter) || e.EbookCover.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EbookNameFilter), e => e.EbookName.ToLower() == input.EbookNameFilter.ToLower().Trim())
                        .WhereIf(input.ProFilter > -1, e => Convert.ToInt32(e.Pro) == input.ProFilter)
                        .WhereIf(input.MinBookPageFilter != null, e => e.BookPage >= input.MinBookPageFilter)
                        .WhereIf(input.MaxBookPageFilter != null, e => e.BookPage <= input.MaxBookPageFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.UserFk != null && e.UserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PbClassClassNameFilter), e => e.PbClassFk != null && e.PbClassFk.ClassName.ToLower() == input.PbClassClassNameFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PbRankRankNameFilter), e => e.PbRankFk != null && e.PbRankFk.RankName.ToLower() == input.PbRankRankNameFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PbTypeEbookTypeNameFilter), e => e.PbTypeEbookFk != null && e.PbTypeEbookFk.TypeName.ToLower() == input.PbTypeEbookTypeNameFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PbTypeFileTypeFileNameFilter), e => e.PbTypeFileFk != null && e.PbTypeFileFk.TypeFileName.ToLower() == input.PbTypeFileTypeFileNameFilter.ToLower().Trim());

            var pagedAndFilteredPbEbooks = filteredPbEbooks
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var pbEbooks = from o in pagedAndFilteredPbEbooks
                           join o1 in _lookup_userRepository.GetAll() on o.UserId equals o1.Id into j1
                           from s1 in j1.DefaultIfEmpty()

                           join o2 in _lookup_pbClassRepository.GetAll() on o.PbClassId equals o2.Id into j2
                           from s2 in j2.DefaultIfEmpty()

                           join o4 in _lookup_pbRankRepository.GetAll() on o.PbRankId equals o4.Id into j4
                           from s4 in j4.DefaultIfEmpty()

                           join o8 in _lookup_pbTypeEbookRepository.GetAll() on o.PbTypeEbookId equals o8.Id into j8
                           from s8 in j8.DefaultIfEmpty()

                           join o9 in _lookup_pbTypeFileRepository.GetAll() on o.PbTypeFileId equals o9.Id into j9
                           from s9 in j9.DefaultIfEmpty()

                           select new GetPbEbookForDescription()
                           {
                               EbookName = o.EbookName,
                               Link = o.Link,
                               EbookDateStart = o.EbookDateStart,
                               Pro = o.Pro,
                               EbookPrice = o.EbookPrice,
                               EbookView = o.EbookView,
                               EbookLike = o.EbookLike,
                               EbookDislike = o.EbookDislike,
                               EbookCover = o.EbookCover,
                               BookPage = o.BookPage,
                               UserName = s1 == null ? "" : s1.Name.ToString(),
                               PbClassClassName = s2 == null ? "" : s2.ClassName.ToString(),
                               PbRankRankName = s4 == null ? "" : s4.RankName.ToString(),
                               PbTypeEbookTypeName = s8 == null ? "" : s8.TypeName.ToString(),
                               PbTypeFileTypeFileName = s9 == null ? "" : s9.TypeFileName.ToString()
                           };

            var totalCount = await filteredPbEbooks.CountAsync();

            return new PagedResultDto<GetPbEbookForDescription>(
                totalCount,
                await pbEbooks.ToListAsync()
            );
        }
        public async Task<GetPbEbookForViewDto> GetPbEbookForView(int id)
         {
            var pbEbook = await _pbEbookRepository.GetAsync(id);

            var output = new GetPbEbookForViewDto { PbEbook = ObjectMapper.Map<PbEbookDto>(pbEbook) };

		    if (output.PbEbook.UserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.PbEbook.UserId);
                output.UserName = _lookupUser.Name.ToString();
            }

		    if (output.PbEbook.PbClassId != null)
            {
                var _lookupPbClass = await _lookup_pbClassRepository.FirstOrDefaultAsync((int)output.PbEbook.PbClassId);
                output.PbClassClassName = _lookupPbClass.ClassName.ToString();
            }

		    if (output.PbEbook.PbPlaceId != null)
            {
                var _lookupPbPlace = await _lookup_pbPlaceRepository.FirstOrDefaultAsync((int)output.PbEbook.PbPlaceId);
                output.PbPlacePlaceName = _lookupPbPlace.PlaceName.ToString();
            }

		    if (output.PbEbook.PbRankId != null)
            {
                var _lookupPbRank = await _lookup_pbRankRepository.FirstOrDefaultAsync((int)output.PbEbook.PbRankId);
                output.PbRankRankName = _lookupPbRank.RankName.ToString();
            }

		    if (output.PbEbook.PbStatusId != null)
            {
                var _lookupPbStatus = await _lookup_pbStatusRepository.FirstOrDefaultAsync((int)output.PbEbook.PbStatusId);
                output.PbStatusStatusName = _lookupPbStatus.StatusName.ToString();
            }

		    if (output.PbEbook.PbSubjectId != null)
            {
                var _lookupPbSubject = await _lookup_pbSubjectRepository.FirstOrDefaultAsync((int)output.PbEbook.PbSubjectId);
                output.PbSubjectSectionName = _lookupPbSubject.SectionName.ToString();
            }

		    if (output.PbEbook.PbSubjectEducationId != null)
            {
                var _lookupPbSubjectEducation = await _lookup_pbSubjectEducationRepository.FirstOrDefaultAsync((int)output.PbEbook.PbSubjectEducationId);
                output.PbSubjectEducationSubjectName = _lookupPbSubjectEducation.SubjectName.ToString();
            }

		    if (output.PbEbook.PbTypeEbookId != null)
            {
                var _lookupPbTypeEbook = await _lookup_pbTypeEbookRepository.FirstOrDefaultAsync((int)output.PbEbook.PbTypeEbookId);
                output.PbTypeEbookTypeName = _lookupPbTypeEbook.TypeName.ToString();
            }

		    if (output.PbEbook.PbTypeFileId != null)
            {
                var _lookupPbTypeFile = await _lookup_pbTypeFileRepository.FirstOrDefaultAsync((int)output.PbEbook.PbTypeFileId);
                output.PbTypeFileTypeFileName = _lookupPbTypeFile.TypeFileName.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_PbEbooks_Edit)]
		 public async Task<GetPbEbookForEditOutput> GetPbEbookForEdit(EntityDto input)
         {
            var pbEbook = await _pbEbookRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPbEbookForEditOutput {PbEbook = ObjectMapper.Map<CreateOrEditPbEbookDto>(pbEbook)};

		    if (output.PbEbook.UserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.PbEbook.UserId);
                output.UserName = _lookupUser.Name.ToString();
            }

		    if (output.PbEbook.PbClassId != null)
            {
                var _lookupPbClass = await _lookup_pbClassRepository.FirstOrDefaultAsync((int)output.PbEbook.PbClassId);
                output.PbClassClassName = _lookupPbClass.ClassName.ToString();
            }

		    if (output.PbEbook.PbPlaceId != null)
            {
                var _lookupPbPlace = await _lookup_pbPlaceRepository.FirstOrDefaultAsync((int)output.PbEbook.PbPlaceId);
                output.PbPlacePlaceName = _lookupPbPlace.PlaceName.ToString();
            }

		    if (output.PbEbook.PbRankId != null)
            {
                var _lookupPbRank = await _lookup_pbRankRepository.FirstOrDefaultAsync((int)output.PbEbook.PbRankId);
                output.PbRankRankName = _lookupPbRank.RankName.ToString();
            }

		    if (output.PbEbook.PbStatusId != null)
            {
                var _lookupPbStatus = await _lookup_pbStatusRepository.FirstOrDefaultAsync((int)output.PbEbook.PbStatusId);
                output.PbStatusStatusName = _lookupPbStatus.StatusName.ToString();
            }

		    if (output.PbEbook.PbSubjectId != null)
            {
                var _lookupPbSubject = await _lookup_pbSubjectRepository.FirstOrDefaultAsync((int)output.PbEbook.PbSubjectId);
                output.PbSubjectSectionName = _lookupPbSubject.SectionName.ToString();
            }

		    if (output.PbEbook.PbSubjectEducationId != null)
            {
                var _lookupPbSubjectEducation = await _lookup_pbSubjectEducationRepository.FirstOrDefaultAsync((int)output.PbEbook.PbSubjectEducationId);
                output.PbSubjectEducationSubjectName = _lookupPbSubjectEducation.SubjectName.ToString();
            }

		    if (output.PbEbook.PbTypeEbookId != null)
            {
                var _lookupPbTypeEbook = await _lookup_pbTypeEbookRepository.FirstOrDefaultAsync((int)output.PbEbook.PbTypeEbookId);
                output.PbTypeEbookTypeName = _lookupPbTypeEbook.TypeName.ToString();
            }

		    if (output.PbEbook.PbTypeFileId != null)
            {
                var _lookupPbTypeFile = await _lookup_pbTypeFileRepository.FirstOrDefaultAsync((int)output.PbEbook.PbTypeFileId);
                output.PbTypeFileTypeFileName = _lookupPbTypeFile.TypeFileName.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPbEbookDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_PbEbooks_Create)]
		 protected virtual async Task Create(CreateOrEditPbEbookDto input)
         {
            var pbEbook = ObjectMapper.Map<PbEbook>(input);

			

            await _pbEbookRepository.InsertAsync(pbEbook);
         }

		 [AbpAuthorize(AppPermissions.Pages_PbEbooks_Edit)]
		 protected virtual async Task Update(CreateOrEditPbEbookDto input)
         {
            var pbEbook = await _pbEbookRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, pbEbook);
         }

		 [AbpAuthorize(AppPermissions.Pages_PbEbooks_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _pbEbookRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetPbEbooksToExcel(GetAllPbEbooksForExcelInput input)
         {
			
			var filteredPbEbooks = _pbEbookRepository.GetAll()
						.Include( e => e.UserFk)
						.Include( e => e.PbClassFk)
						.Include( e => e.PbPlaceFk)
						.Include( e => e.PbRankFk)
						.Include( e => e.PbStatusFk)
						.Include( e => e.PbSubjectFk)
						.Include( e => e.PbSubjectEducationFk)
						.Include( e => e.PbTypeEbookFk)
						.Include( e => e.PbTypeFileFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.EbookName.Contains(input.Filter) || e.Link.Contains(input.Filter) || e.Discription.Contains(input.Filter) || e.EbookCover.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.EbookNameFilter),  e => e.EbookName.ToLower() == input.EbookNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.LinkFilter),  e => e.Link.ToLower() == input.LinkFilter.ToLower().Trim())
						.WhereIf(input.MinEbookDateStartFilter != null, e => e.EbookDateStart >= input.MinEbookDateStartFilter)
						.WhereIf(input.MaxEbookDateStartFilter != null, e => e.EbookDateStart <= input.MaxEbookDateStartFilter)
						.WhereIf(input.ProFilter > -1,  e => Convert.ToInt32(e.Pro) == input.ProFilter )
						.WhereIf(input.MinEbookPriceFilter != null, e => e.EbookPrice >= input.MinEbookPriceFilter)
						.WhereIf(input.MaxEbookPriceFilter != null, e => e.EbookPrice <= input.MaxEbookPriceFilter)
						.WhereIf(input.MinEbookViewFilter != null, e => e.EbookView >= input.MinEbookViewFilter)
						.WhereIf(input.MaxEbookViewFilter != null, e => e.EbookView <= input.MaxEbookViewFilter)
						.WhereIf(input.MinEbookLikeFilter != null, e => e.EbookLike >= input.MinEbookLikeFilter)
						.WhereIf(input.MaxEbookLikeFilter != null, e => e.EbookLike <= input.MaxEbookLikeFilter)
						.WhereIf(input.MinEbookDislikeFilter != null, e => e.EbookDislike >= input.MinEbookDislikeFilter)
						.WhereIf(input.MaxEbookDislikeFilter != null, e => e.EbookDislike <= input.MaxEbookDislikeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiscriptionFilter),  e => e.Discription.ToLower() == input.DiscriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.EbookCoverFilter),  e => e.EbookCover.ToLower() == input.EbookCoverFilter.ToLower().Trim())
						.WhereIf(input.MinBookPageFilter != null, e => e.BookPage >= input.MinBookPageFilter)
						.WhereIf(input.MaxBookPageFilter != null, e => e.BookPage <= input.MaxBookPageFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.UserFk != null && e.UserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbClassClassNameFilter), e => e.PbClassFk != null && e.PbClassFk.ClassName.ToLower() == input.PbClassClassNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbPlacePlaceNameFilter), e => e.PbPlaceFk != null && e.PbPlaceFk.PlaceName.ToLower() == input.PbPlacePlaceNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbRankRankNameFilter), e => e.PbRankFk != null && e.PbRankFk.RankName.ToLower() == input.PbRankRankNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbStatusStatusNameFilter), e => e.PbStatusFk != null && e.PbStatusFk.StatusName.ToLower() == input.PbStatusStatusNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbSubjectSectionNameFilter), e => e.PbSubjectFk != null && e.PbSubjectFk.SectionName.ToLower() == input.PbSubjectSectionNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbSubjectEducationSubjectNameFilter), e => e.PbSubjectEducationFk != null && e.PbSubjectEducationFk.SubjectName.ToLower() == input.PbSubjectEducationSubjectNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbTypeEbookTypeNameFilter), e => e.PbTypeEbookFk != null && e.PbTypeEbookFk.TypeName.ToLower() == input.PbTypeEbookTypeNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbTypeFileTypeFileNameFilter), e => e.PbTypeFileFk != null && e.PbTypeFileFk.TypeFileName.ToLower() == input.PbTypeFileTypeFileNameFilter.ToLower().Trim());

			var query = (from o in filteredPbEbooks
                         join o1 in _lookup_userRepository.GetAll() on o.UserId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_pbClassRepository.GetAll() on o.PbClassId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         join o3 in _lookup_pbPlaceRepository.GetAll() on o.PbPlaceId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()
                         
                         join o4 in _lookup_pbRankRepository.GetAll() on o.PbRankId equals o4.Id into j4
                         from s4 in j4.DefaultIfEmpty()
                         
                         join o5 in _lookup_pbStatusRepository.GetAll() on o.PbStatusId equals o5.Id into j5
                         from s5 in j5.DefaultIfEmpty()
                         
                         join o6 in _lookup_pbSubjectRepository.GetAll() on o.PbSubjectId equals o6.Id into j6
                         from s6 in j6.DefaultIfEmpty()
                         
                         join o7 in _lookup_pbSubjectEducationRepository.GetAll() on o.PbSubjectEducationId equals o7.Id into j7
                         from s7 in j7.DefaultIfEmpty()
                         
                         join o8 in _lookup_pbTypeEbookRepository.GetAll() on o.PbTypeEbookId equals o8.Id into j8
                         from s8 in j8.DefaultIfEmpty()
                         
                         join o9 in _lookup_pbTypeFileRepository.GetAll() on o.PbTypeFileId equals o9.Id into j9
                         from s9 in j9.DefaultIfEmpty()
                         
                         select new GetPbEbookForViewDto() { 
							PbEbook = new PbEbookDto
							{
                                EbookName = o.EbookName,
                                Link = o.Link,
                                EbookDateStart = o.EbookDateStart,
                                Pro = o.Pro,
                                EbookPrice = o.EbookPrice,
                                EbookView = o.EbookView,
                                EbookLike = o.EbookLike,
                                EbookDislike = o.EbookDislike,
                                Discription = o.Discription,
                                EbookCover = o.EbookCover,
                                BookPage = o.BookPage,
                                Id = o.Id
							},
                         	UserName = s1 == null ? "" : s1.Name.ToString(),
                         	PbClassClassName = s2 == null ? "" : s2.ClassName.ToString(),
                         	PbPlacePlaceName = s3 == null ? "" : s3.PlaceName.ToString(),
                         	PbRankRankName = s4 == null ? "" : s4.RankName.ToString(),
                         	PbStatusStatusName = s5 == null ? "" : s5.StatusName.ToString(),
                         	PbSubjectSectionName = s6 == null ? "" : s6.SectionName.ToString(),
                         	PbSubjectEducationSubjectName = s7 == null ? "" : s7.SubjectName.ToString(),
                         	PbTypeEbookTypeName = s8 == null ? "" : s8.TypeName.ToString(),
                         	PbTypeFileTypeFileName = s9 == null ? "" : s9.TypeFileName.ToString()
						 });


            var pbEbookListDtos = await query.ToListAsync();

            return _pbEbooksExcelExporter.ExportToFile(pbEbookListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_PbEbooks)]
         public async Task<PagedResultDto<PbEbookUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_userRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<PbEbookUserLookupTableDto>();
			foreach(var user in userList){
				lookupTableDtoList.Add(new PbEbookUserLookupTableDto
				{
					Id = user.Id,
					DisplayName = user.Name?.ToString()
				});
			}

            return new PagedResultDto<PbEbookUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_PbEbooks)]
         public async Task<PagedResultDto<PbEbookPbClassLookupTableDto>> GetAllPbClassForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_pbClassRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.ClassName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var pbClassList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<PbEbookPbClassLookupTableDto>();
			foreach(var pbClass in pbClassList){
				lookupTableDtoList.Add(new PbEbookPbClassLookupTableDto
				{
					Id = pbClass.Id,
					DisplayName = pbClass.ClassName?.ToString()
				});
			}

            return new PagedResultDto<PbEbookPbClassLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_PbEbooks)]
         public async Task<PagedResultDto<PbEbookPbPlaceLookupTableDto>> GetAllPbPlaceForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_pbPlaceRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.PlaceName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var pbPlaceList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<PbEbookPbPlaceLookupTableDto>();
			foreach(var pbPlace in pbPlaceList){
				lookupTableDtoList.Add(new PbEbookPbPlaceLookupTableDto
				{
					Id = pbPlace.Id,
					DisplayName = pbPlace.PlaceName?.ToString()
				});
			}

            return new PagedResultDto<PbEbookPbPlaceLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_PbEbooks)]
         public async Task<PagedResultDto<PbEbookPbRankLookupTableDto>> GetAllPbRankForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_pbRankRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.RankName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var pbRankList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<PbEbookPbRankLookupTableDto>();
			foreach(var pbRank in pbRankList){
				lookupTableDtoList.Add(new PbEbookPbRankLookupTableDto
				{
					Id = pbRank.Id,
					DisplayName = pbRank.RankName?.ToString()
				});
			}

            return new PagedResultDto<PbEbookPbRankLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_PbEbooks)]
         public async Task<PagedResultDto<PbEbookPbStatusLookupTableDto>> GetAllPbStatusForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_pbStatusRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.StatusName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var pbStatusList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<PbEbookPbStatusLookupTableDto>();
			foreach(var pbStatus in pbStatusList){
				lookupTableDtoList.Add(new PbEbookPbStatusLookupTableDto
				{
					Id = pbStatus.Id,
					DisplayName = pbStatus.StatusName?.ToString()
				});
			}

            return new PagedResultDto<PbEbookPbStatusLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_PbEbooks)]
         public async Task<PagedResultDto<PbEbookPbSubjectLookupTableDto>> GetAllPbSubjectForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_pbSubjectRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.SectionName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var pbSubjectList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<PbEbookPbSubjectLookupTableDto>();
			foreach(var pbSubject in pbSubjectList){
				lookupTableDtoList.Add(new PbEbookPbSubjectLookupTableDto
				{
					Id = pbSubject.Id,
					DisplayName = pbSubject.SectionName?.ToString()
				});
			}

            return new PagedResultDto<PbEbookPbSubjectLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_PbEbooks)]
         public async Task<PagedResultDto<PbEbookPbSubjectEducationLookupTableDto>> GetAllPbSubjectEducationForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_pbSubjectEducationRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.SubjectName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var pbSubjectEducationList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<PbEbookPbSubjectEducationLookupTableDto>();
			foreach(var pbSubjectEducation in pbSubjectEducationList){
				lookupTableDtoList.Add(new PbEbookPbSubjectEducationLookupTableDto
				{
					Id = pbSubjectEducation.Id,
					DisplayName = pbSubjectEducation.SubjectName?.ToString()
				});
			}

            return new PagedResultDto<PbEbookPbSubjectEducationLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_PbEbooks)]
         public async Task<PagedResultDto<PbEbookPbTypeEbookLookupTableDto>> GetAllPbTypeEbookForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_pbTypeEbookRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TypeName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var pbTypeEbookList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<PbEbookPbTypeEbookLookupTableDto>();
			foreach(var pbTypeEbook in pbTypeEbookList){
				lookupTableDtoList.Add(new PbEbookPbTypeEbookLookupTableDto
				{
					Id = pbTypeEbook.Id,
					DisplayName = pbTypeEbook.TypeName?.ToString()
				});
			}

            return new PagedResultDto<PbEbookPbTypeEbookLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_PbEbooks)]
         public async Task<PagedResultDto<PbEbookPbTypeFileLookupTableDto>> GetAllPbTypeFileForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_pbTypeFileRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TypeFileName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var pbTypeFileList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<PbEbookPbTypeFileLookupTableDto>();
			foreach(var pbTypeFile in pbTypeFileList){
				lookupTableDtoList.Add(new PbEbookPbTypeFileLookupTableDto
				{
					Id = pbTypeFile.Id,
					DisplayName = pbTypeFile.TypeFileName?.ToString()
				});
			}

            return new PagedResultDto<PbEbookPbTypeFileLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}