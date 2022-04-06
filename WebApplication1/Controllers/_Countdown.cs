namespace WebApplication1.Controllers
{
    public class _Countdown
    {
        private List<Orders> orders;
        private List<Orders> ordersOnPeriod;
        private List<List<string>> usedProducts;
        private DateTime from;
        private DateTime to;
        private double allPriceSelledDishes;
        private double allPriceUsedProduct;
        private double totalPrice;
        private bool hasOrder;

        public List<Orders> Orders
        {
            get { return orders; }
            set { orders = value; }
        }
        public List<Orders> OrdersOnPeriod
        {
            get { return ordersOnPeriod; }
            set { ordersOnPeriod = value; }
        }
        public List<List<string>> UsedProducts
        {
            get { return usedProducts; }
            set { usedProducts = value; }
        }
        public DateTime From
        {
            get { return from; }
            set { from = value; }
        }
        public DateTime To
        {
            get { return to; }
            set { to = value; }
        }
        public double AllPriceSelledDishes
        {
            get { return allPriceSelledDishes; }
            set { allPriceSelledDishes = value; }
        }
        public double AllPriceUsedProduct
        {
            get { return allPriceUsedProduct; }
            set { allPriceUsedProduct = value; }
        }
        public double TotalPrice
        {
            get { return totalPrice; }
            private set { totalPrice = value; }
        }
        public bool HasOrder
        {
            get { return hasOrder; }
            set { hasOrder = value; }
        }
        public _Countdown()
        {
        }
        public _Countdown(List<Orders> orders,DateTime from,DateTime to)
        {
            _DataBase _dataBase = new _DataBase();
            List<List<string>> tempUsedProduct = new List<List<string>>();
            hasOrder = true;
            this.orders = orders;
            this.from = from;
            this.to = to;
            ordersOnPeriod = new List<Orders>();
            int tempNumberOrder = -1;
            for (int i = 0,c = 0; i < orders.Count; i++)
            {
                if(orders[i].Date >= from && orders[i].Date <= to)
                {
                    ordersOnPeriod.Add(orders[i]);
                    allPriceSelledDishes += int.Parse(orders[i].PriceOrder);

                    if(tempNumberOrder == orders[i].NumberOrder)
                    {
                        c = 1;
                    }
                    else
                    {
                        c = 0;
                    }
                    if (c==0)
                    {
                        tempNumberOrder = orders[i].NumberOrder;
                        List<string> temp = _dataBase.ReadFromDataBase($"exec usedProductInOrder {tempNumberOrder}");
                        tempUsedProduct.Add(temp);
                    }
                }
            }

            // to filter used products here

            List<List<string>> usedProducts1 = new List<List<string>>();
            for(int i = 0,c = 0; i < tempUsedProduct.Count; i++,c++)
            {
                int k = 0;
                List<string> tempProduct = new List<string>();
                string tempNameProduct = "";
                for (int j = 0; j < tempUsedProduct[i].Count; j++)
                {
                    bool isSame = false;
                    if(j % 4 == 0)
                    {
                        if (c != 0)
                        {
                            for(int j1 = 0; j1 < usedProducts1[0].Count; j1+=4)
                            {
                                if (usedProducts1[0][j1] == tempUsedProduct[i][j])
                                {
                                    double tempCount = Convert.ToDouble(usedProducts1[0][j1+1]) + Convert.ToDouble(tempUsedProduct[i][j+1]);
                                    usedProducts1[0][j1+1] = tempCount.ToString("0.00");
                                    double tempCost = Convert.ToDouble(usedProducts1[0][j1+3]) + Convert.ToDouble(tempUsedProduct[i][j+3]);
                                    usedProducts1[0][j1+3] = tempCost.ToString("0.00");
                                    isSame = true;
                                    break;
                                }
                            }
                        }
                        if (!isSame)
                        {
                            if (tempNameProduct == tempUsedProduct[i][j])
                            {
                                double tempCount = Convert.ToDouble(tempProduct[4*k - 3]) + Convert.ToDouble(tempUsedProduct[i][j+1]);
                                tempProduct[4*k - 3] = tempCount.ToString("0.00");
                                double tempCost = Convert.ToDouble(tempProduct[4*k - 1]) + Convert.ToDouble(tempUsedProduct[i][j+3]);
                                tempProduct[4*k - 1] = tempCost.ToString("0.00");

                                continue;
                            }
                            else
                            {
                                tempProduct.Add(tempUsedProduct[i][j]);
                                double tempCount = Convert.ToDouble(tempUsedProduct[i][j+1]);
                                tempProduct.Add(tempCount.ToString("0.00"));
                                tempProduct.Add(tempUsedProduct[i][j+2]);
                                double tempCost = Convert.ToDouble(tempUsedProduct[i][j+3]);
                                tempProduct.Add(tempCost.ToString("0.00"));
                                k++;
                                tempNameProduct = tempUsedProduct[i][j];
                            }

                        }
                        isSame = false;
                    }
                }
                if (c == 0)
                {
                    usedProducts1.Add(tempProduct);
                }
                else
                {
                    for(int i1 = 0; i1 < tempProduct.Count; i1++)
                    {
                        usedProducts1[0].Add(tempProduct[i1]);
                    }
                }
                c++;
            }

            for(int i = 3; i < usedProducts1[0].Count; i+=4)
            {
                usedProducts1[0][i] += " - com";
                //usedProducts1[0][i-1] += " - com";
            }
            for(int i = 1; i < usedProducts1[0].Count; i+=4)
            {
                int tempUnitCode = int.Parse(_dataBase.ReadFromDataBaseOneItem("product", "code_unit",$"name_product = '{usedProducts1[0][i-1]}'"));
                usedProducts1[0][i] += " - " + _dataBase.ReadFromDataBaseOneItem("units", "name_unit",$"code_unit = {tempUnitCode}");
            }

            this.usedProducts = usedProducts1;
            this.allPriceUsedProduct = Convert.ToDouble(_dataBase.ReadFromDataBaseOneItem($"exec allPriceUsedProducts '{from}','{to}'"));
            this.totalPrice = this.allPriceSelledDishes - this.allPriceUsedProduct;
        }

    }
}
