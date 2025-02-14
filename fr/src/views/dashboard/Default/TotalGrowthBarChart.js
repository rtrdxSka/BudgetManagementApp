import PropTypes from 'prop-types';
import { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';

// material-ui
import { Grid, MenuItem, TextField } from '@mui/material';
import { useTheme } from '@mui/material/styles';

// third-party
import ApexCharts from 'apexcharts';
import Chart from 'react-apexcharts';

// project imports
import { gridSpacing } from 'store/constant';
import MainCard from 'ui-component/cards/MainCard';
import SkeletonTotalGrowthBarChart from 'ui-component/cards/Skeleton/TotalGrowthBarChart';

// chart data
import { useQuery } from '@tanstack/react-query';
import axios from 'axios';





const years = [
  {
    value: '2023',
    label: '2023'
  },
  {
    value: '2024',
    label: '2024'
  },
  {
    value: '2025',
    label: '2025'
  }
];
// ==============================|| DASHBOARD DEFAULT - TOTAL GROWTH BAR CHART ||============================== //

const TotalGrowthBarChart = () => {

  const getChartData = async () => {
    const response = await axios.get("http://localhost:5001/api/Expenses/data", {
      params: {
        email: localStorage.getItem("email"),
      }
    });
    return response.data;
  }
  const {data: realChart, isLoading} = useQuery({
    queryKey: ["realChart"], 
    queryFn: getChartData
  })

  

  const chartData = {
    height: 480,
    type: 'bar',
    options: {
      chart: {
        id: 'bar-chart',
        stacked: true,
        toolbar: {
          show: true
        },
        zoom: {
          enabled: true
        }
      },
      responsive: [
        {
          breakpoint: 480,
          options: {
            legend: {
              position: 'bottom',
              offsetX: -10,
              offsetY: 0
            }
          }
        }
      ],
      plotOptions: {
        bar: {
          horizontal: false,
          columnWidth: '50%'
        }
      },
      xaxis: {
        type: 'category',
        categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
      },
      legend: {
        show: true,
        fontSize: '14px',
        fontFamily: `'Roboto', sans-serif`,
        position: 'bottom',
        offsetX: 20,
        labels: {
          useSeriesColors: false
        },
        markers: {
          width: 16,
          height: 16,
          radius: 5
        },
        itemMargin: {
          horizontal: 15,
          vertical: 8
        }
      },
      fill: {
        type: 'solid'
      },
      dataLabels: {
        enabled: false
      },
      grid: {
        show: true
      }
    },
    series: realChart
  };

  // Update ApexCharts when chartData changes
  useEffect(() => {
    if (!isLoading && chartData) {
      const updatedChartData = {
        ...chartData,
        series: chartData.series // Assuming 'chartData' from the server is structured correctly for the ApexChart
      };
      ApexCharts.exec('bar-chart', 'updateOptions', updatedChartData);
    }
  }, [chartData, isLoading]);

  const [value, setValue] = useState('income');
  const [month, setMonth] = useState('1');
  const [year, setYear] = useState('2024');
  const theme = useTheme();
  const customization = useSelector((state) => state.customization);
  console.log(setValue)
  console.log(month)
  console.log(setMonth)
  const { navType } = customization;
  const { primary } = theme.palette.text;
  const darkLight = theme.palette.dark.light;
  const grey200 = theme.palette.grey[200];
  const grey500 = theme.palette.grey[500];

  const primary200 = theme.palette.primary[200];
  const primaryDark = theme.palette.primary.dark;
  const secondaryMain = theme.palette.secondary.main;
  const secondaryLight = theme.palette.secondary.light;

  

  const getData = async () => {
    const response = await axios.get("http://localhost:5001/api/Expenses", {
      params: {
        email: localStorage.getItem("email"),
        type: value
      }
    });
    return response.data;
  }

  const {data, refetch} = useQuery({
    queryKey: ["budget"], 
    queryFn: getData
  })

  useEffect(() => {
    const newChartData = {
      ...chartData.options,
      colors: [primary200, primaryDark, secondaryMain, secondaryLight],
      xaxis: {
        labels: {
          style: {
            colors: [primary, primary, primary, primary, primary, primary, primary, primary, primary, primary, primary, primary]
          }
        }
      },
      yaxis: {
        labels: {
          style: {
            colors: [primary]
          }
        }
      },
      grid: {
        borderColor: grey200
      },
      tooltip: {
        theme: 'light'
      },
      legend: {
        labels: {
          colors: grey500
        }
      }
    };

    // do not load chart when loading
    if (!isLoading) {
      ApexCharts.exec(`bar-chart`, 'updateOptions', newChartData);
    }
  }, [navType, primary200, primaryDark, secondaryMain, secondaryLight, primary, darkLight, grey200, isLoading, grey500]);

  return (
    <>
      {isLoading ? (
        <SkeletonTotalGrowthBarChart />
      ) : (
        <MainCard>
          <Grid container spacing={gridSpacing}>
            <Grid item xs={12}>
              <Grid container alignItems="center" justifyContent="space-between">
                <Grid item>
                  <Grid container direction="column" spacing={1}>
                  </Grid>
                </Grid>
                <Grid item style = {{display: 'flex', alignItems: 'center', gap: '1rem'}}>
                  <TextField select value={year} onChange={(e) => {setYear(e.target.value);refetch();}}>
                    {years.map((option) => (
                      <MenuItem key={option.value} value={option.value}>
                        {option.label}
                      </MenuItem>
                    ))}
                  </TextField>
                </Grid>
              </Grid>
            </Grid>
            <Grid item xs={12}>
              <Chart {...chartData} />
            </Grid>
          </Grid>
        </MainCard>
      )}
    </>
  );
};

TotalGrowthBarChart.propTypes = {
  isLoading: PropTypes.bool
};




export default TotalGrowthBarChart;
