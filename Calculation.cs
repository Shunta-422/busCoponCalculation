using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Calculation
{
    // 往復ボタンを押したときにユーザが入力した回数を2倍にして計算メソッドを呼び出す
    private void roundTrip_clicked(object sender, EventArgs e)
    {
        int price = (int.TryParse(this.oneWayPrice.Text, out price)) ? price : price = 0;
        price = (teiki.ContainsKey(price)) ? price : price = 0;
        if (price <= 0)
        {
            MessageBox.Show($"正しい片道料金を入力してください");
        }

        int count = (int.TryParse(this.oneWayCount.Text, out count)) ? count * 2 : count = 0;
        if (count <= 0)
        {
            MessageBox.Show($"正しい回数を入力してください");
        }

    }
    // int price =>ユーザが入力する片道料金
    // int count =>ユーザが入力する往復回数

    // 片道*回数を計算し計算結果を格納する変数を返すメソッド
    public int SumPrice(int price, int count)
    {
        int sumPrice = price * count;
        return sumPrice;
    }

    // 片道料金の中に100の要素がいくつあるか格納する変数を返すメソッド
    public int HundredElement(int price)
    {
        int hundredElement = price / 100;
        return hundredElement;
    }

    // 片道料金の中にある110~190などの10の位が含まれる数を覚えておく変数を返すメソッド
    public int Remainder(int price)
    {
        int remainder = price % 100;
        return remainder;
    }
    

    // 上の変数を利用しユーザが入力した値を引数として計算した値をList<int>　dataで返すメソッド
    public List<int> Calculation(int price,int count)
	{
        List<int> data = new List<int>();
        int sumPrice = SumPrice(price, count);
        int hundredElement = HundredElement(price);
        int remainder = Remainder(price);
        int countRemainder = 0;
        int hE;  //100が何回使われるか数える
        int cR;  //10の位の2～9が使われる回数を数える
        int hundredSet;//100円券セットが何セット必要かを代入する変数
        int remainderSet;//100円券意外のセットが何セット必要かを代入する変数
        int residueHE;//100円セット意外に何枚の100円券が必要か数える変数
        int residueCR;//100円意外のセットの他に何枚券が必要か数える変数

        if (price % 100 == 0) // 100で割り切れるパターン
        {
            hE = count * hundredElement;
            hundredSet = hE / 12;
            residueHE = hE - hundredSet * 12;

            data.Add(hundredSet); // data[0]100円のセット券の枚数
            data.Add(residueHE); // data[1]セット以外で用意しなければならない100円券の数
            data.Add(hundredSet * 1000 + residueHE * 100); // data[2]合計金額
            return data;
        }

        else if (hundredElement > 1) // 100円のセット券と他のセット券を使うパターン
        {
            hundredElement -= 1;
            countRemainder += 1;
            remainder += 100;
            hE = count * hundredElement;
            cR = count * countRemainder;
            hundredSet = hE / 12;   //100円券は12枚綴り
            remainderSet = cR / 11; //100円券意外は11枚綴り
            residueHE = hE - hundredSet * 12;
            residueCR = cR - remainderSet * 11;

            data.Add(count * hundredElement); // data[0]100円券の枚数
            data.Add(remainder);// data[1]10の位が0じゃない券の名前
            data.Add(count * countRemainder);// data[2]data[1]の枚数
            data.Add(hE / 12);// data[3]100円券のセットの数
            data.Add(cR / 11);// data[4]100円券意外のセットの数
            data.Add(hE - hundredSet * 12);// data[5]セット以外で用意しなければならない100円券の数
            data.Add(cR - remainderSet * 11);// data[6]セット意外で用意しなければならない100円以外の券の数
            data.Add(1000 * hundredSet + remainder * 10 * remainderSet + 100 * residueHE + remainder * residueCR);// data[7]全部で掛かる値段
            /*data.Add(teikiPrice);// data[8]定期1か月分の値段*/

            /*MessageBox.Show("100円券が" + data[0] + "枚です。\r\n" + data[1] + "円券が" + data[2] + "枚です。\r\n" +
                "100円券が" + data[3] + "セット、" + data[1] + "券が" + data[4] + "セットです。\r\n" +
                "乗車券を別で100円券を" + data[5] + "枚と" + data[1] + "券を別で" + data[6] + "枚購入してください。\r\n" +
                "全部で" + data[7] + "円です\r\n" +
                "定期だと" + teikiString[oneWayPrice.Text] + "円です");*/
            return data;
        }
        else // 100円のセット券を使わないパターン
        {
            countRemainder += 1;
            remainder += 100;
            cR = countRemainder * count;
            remainderSet = cR / 11;
            residueCR = cR - remainderSet * 11;

            data.Add(remainder);// data[0]10のくらい以外の回数券の名前
            data.Add(countRemainder * count);// data[1]回数券の枚数
            data.Add(cR / 11);// data[2]セット数                
            data.Add(cR - remainderSet * 11);// data[3]セットからあぶれた回数券の枚数
            data.Add(remainder * 10 * remainderSet + remainder * residueCR);// data[4]全部で掛かる値段

            /*MessageBox.Show(data[0] + "円券が" + data[1] + "枚です。\r\n" +
                data[0] + "券が" + data[2] + "セットです。\r\n" +
                data[0] + "券を別で" + data[3] + "枚購入してください。\r\n" +
                "全部で" + data[4] + "円です\r\n" +
                "定期だと" + teikiString[oneWayPrice.Text] + "円です");*/
            return data;
        }
    }
}

