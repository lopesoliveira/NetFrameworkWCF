using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoLib.Contracts;
using GeoLib.Data;

namespace GeoLib.Services
{
    public class GeoManager : IGeoService
    {
        public GeoManager()
        {
        }

        public GeoManager(IZipCodeRepository zipCodeRepository)
        {
            _ZipCodeRepository = zipCodeRepository;
        }

        public GeoManager(IStateRepository stateRepository)
        {
            _IStateRepository = stateRepository;
        }

        public GeoManager(IZipCodeRepository zipCodeRepository, IStateRepository stateRepository)
        {
            _ZipCodeRepository = zipCodeRepository;
            _IStateRepository = stateRepository;
        }

        IZipCodeRepository _ZipCodeRepository = null;
        IStateRepository _IStateRepository = null;

        public ZipCodeData GetZipInfo(string zip)
        {
            
            ZipCodeData zipCodeData = null;
            IZipCodeRepository zipCodeRepository = _ZipCodeRepository ?? new ZipCodeRepository();
            ZipCode zipCodeEntity = zipCodeRepository.GetByZip(zip);
            if (zipCodeEntity != null)
            {
                zipCodeData = new ZipCodeData()
                {
                    City = zipCodeEntity.City,
                    State = zipCodeEntity.State.Abbreviation,
                    ZipCode = zipCodeEntity.Zip
                };
            }

            return zipCodeData;
        }

        public IEnumerable<string> GetStates(bool primaryOnly)
        {
            /*IStateRepository stateCodeRepository = new StateRepository();
            IEnumerable<State> states = stateCodeRepository.Get(true);
            IEnumerable<string> res = states.Select(s => s.Abbreviation).ToList();
            return res;*/

            List<string> stateData = new List<string>();
            IStateRepository stateRepository = _IStateRepository ?? new StateRepository();
            IEnumerable<State> states = stateRepository.Get(primaryOnly);
            if(states != null)
            {
                foreach (State state in states)
                {
                    stateData.Add(state.Abbreviation);
                }
            }

            return stateData;
        }

        public IEnumerable<ZipCodeData> GetZips(string state)
        {
            //throw new NotImplementedException();
            /*
            IStateRepository stateRepository = new StateRepository();
            State states = stateRepository.Get(state);
            IZipCodeRepository zipsRepository = new ZipCodeRepository();
            zipsRepository.
            */

            /*
            IZipCodeRepository zipsRepository = new ZipCodeRepository();
            IEnumerable<ZipCode> zipCodes = zipsRepository.GetByState(state);

            IEnumerable<ZipCodeData> zipCodeDatas = null;
            ZipCodeData zipCodeData = null;
            foreach (ZipCode zipCode in zipCodes)
            {
                zipCodeData.City = zipCode.City;
                zipCodeData.State = state;
                zipCodeData.ZipCode = zipCode.Zip.ToString();
                zipCodeDatas.Append(zipCodeData);
            }

            return zipCodeDatas;
            */

            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();
            IZipCodeRepository zipCodeRepository = _ZipCodeRepository ?? new ZipCodeRepository();

            var zips = zipCodeRepository.GetByState(state);
            if (zips != null)
            {
                foreach(ZipCode zipCode in zips)
                {
                    zipCodeData.Add(new ZipCodeData()
                    {
                        City = zipCode.City,
                        State = zipCode.State.Abbreviation,
                        ZipCode = zipCode.Zip
                    });
                }
            }

            return zipCodeData;
        }

        public IEnumerable<ZipCodeData> GetZips(string zip, int range)
        {
            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();
            IZipCodeRepository zipCodeRepository = _ZipCodeRepository ?? new ZipCodeRepository();

            ZipCode zipEntity = zipCodeRepository.GetByZip(zip);
            IEnumerable<ZipCode> zips = zipCodeRepository.GetZipsForRange(zipEntity, range);
            if (zips != null)
            {
                foreach (ZipCode zipCode in zips)
                {
                    zipCodeData.Add(new ZipCodeData()
                    {
                        City = zipCode.City,
                        State = zipCode.State.Abbreviation,
                        ZipCode = zipCode.Zip
                    });
                }
            }

            return zipCodeData;
        }
    }

}
