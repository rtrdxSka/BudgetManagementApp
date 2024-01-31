import { useEffect, useState } from 'react';

// material-ui
import { Grid } from '@mui/material';

// project imports
import { Button, FormControl, InputLabel, MenuItem, Select, TextField } from '@mui/material';
import axios from 'axios';
import { gridSpacing } from 'store/constant';
import EarningCard from './EarningCard';
import TotalGrowthBarChart from './TotalGrowthBarChart';
import TotalOrderLineChartCard from './TotalOrderLineChartCard';
// ==============================|| DEFAULT DASHBOARD ||============================== //



const Dashboard = () => {
  const [isLoading, setLoading] = useState(true);
  useEffect(() => {
    setLoading(false);
  }, []);

  const [amount, setAmount] = useState('');
  const [type, setType] = useState('');
  const [category, setCategory] = useState('');
  const [date, setDate] = useState(new Date());

  const handleDateChange = (newValue) => {
    setDate(newValue);
  };

  // Form submission handler
  const handleSubmit = async (event) => {
    event.preventDefault();
    const dateValue = new Date(date + 'T00:00:00Z'); // Append 'T00:00:00Z' to set the time to 00:00:00 UTC

  // Now convert it to an ISO string
  const dateInUTCString = dateValue.toISOString();
    // Handle the form submission
    const response = await axios.post("http://localhost:5001/api/Expenses", {
      date: dateInUTCString,
      category: category,
      amount: amount,
      type: type,
      email: localStorage.getItem("email"),
      
    });
    console.log({ amount, type, category, date });
  };

  return (
    <Grid container spacing={gridSpacing}>
      <Grid item xs={12}>
        <Grid container spacing={gridSpacing}>
          <Grid item lg={4} md={6} sm={6} xs={12}>
            <EarningCard isLoading={isLoading} />
          </Grid>
          <Grid item lg={4} md={6} sm={6} xs={12}>
            <TotalOrderLineChartCard isLoading={isLoading} />
          </Grid>
          <Grid item lg={4} md={12} sm={12} xs={12}>
          <form onSubmit={handleSubmit} style={{ padding: '16px', backgroundColor: '#fff', borderRadius: '8px' }}>
      <Grid container spacing={gridSpacing}>
        <Grid item xs={12}>
          <TextField
            fullWidth
            label="Amount"
            value={amount}
            onChange={(e) => setAmount(e.target.value)}
            type="number"
            InputLabelProps={{
              shrink: true,
            }}
          />
        </Grid>
        <Grid item xs={12}>
          <FormControl fullWidth>
            <InputLabel>Type</InputLabel>
            <Select
              value={type}
              label="Type"
              onChange={(e) => setType(e.target.value)}
            >
              <MenuItem value="income">Income</MenuItem>
              <MenuItem value="expense">Expense</MenuItem>
            </Select>
          </FormControl>
        </Grid>
        <Grid item xs={12}>
          <TextField
            fullWidth
            label="Category"
            value={category}
            onChange={(e) => setCategory(e.target.value)}
          />
        </Grid>
        <Grid item xs={12}>
        <input type="date" id="start" name="trip-start" value={date} onChange={(e)=>setDate(e.target.value)} min="2023-01-01" max="2025-12-31" />
        </Grid>
        <Grid item xs={12}>
          <Button variant="contained" color="primary" type="submit">
            Submit
          </Button>
        </Grid>
      </Grid>
    </form>
            <Grid container spacing={gridSpacing}>
            </Grid>
          </Grid>
        </Grid>
      </Grid>
      <Grid item xs={12}>
        <Grid container spacing={gridSpacing}>
          <Grid item xs={12} md={8}>
            <TotalGrowthBarChart  />
            
          </Grid>
          

        </Grid>
      </Grid>
    </Grid>
  );
};

export default Dashboard;
