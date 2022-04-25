import "../MainPage.scss";
import Header from "./Header";
import HeadBlock from "./HeadBlock";
// import MenuBoard from './components/Menu';
import ProductCard from "./ProductCard";
import SideMenuList from "./SideMenuList";
import Banner from "./Banner";

const arr = [
  { name: "NameTitle", description: "decsr", price: "12999" },
  { name: "AnotherNameTitle", description: "decsr", price: "12109" },
  { name: "AnotherNameTitle", description: "decsr", price: "12109" },
  { name: "AnotherNameTitle", description: "decsr", price: "12109" },
  { name: "AnotherNameTitle", description: "decsr", price: "12109" },
  { name: "AnotherNameTitle", description: "decsr", price: "12109" },
  { name: "AnotherNameTitle", description: "decsr", price: "12109" },
  { name: "AnotherNameTitle", description: "decsr", price: "12109" },
];
const menu = [
  {
    nameCategory: "asd",
    subCategories: [
      {
        name: "asdasd",
        link: "/asdasd",
      },
    ],
  },
  {
    nameCategory: "asd",
    subCategories: [
      {
        name: "asdasd",
        link: "/asdasd",
      },
    ],
  },
  {
    nameCategory: "ass",
    subCategories: [
      {
        name: "pampers",
        link: "/papmers",
      },
      {
        name: "poops",
        link: "/poops",
      },
    ],
  },
];

function MainPage() {
  return (
    <>
      <div className="MainPage">
        <Header />
        <HeadBlock />
        {/* <MenuBoard /> */}
        <div className="blocks">
          <div className="blockSideMenus">
            {menu.map((sideMenuListItem) => (
              <SideMenuList
                nameCategory={sideMenuListItem.nameCategory}
                subCategories={sideMenuListItem.subCategories}
              />
            ))}
          </div>
          <div className="blockBanners">
            <Banner />
            <Banner />
          </div>
          <div className="blockCards">
            {arr.map((productCardItem) => (
              <ProductCard
                name={productCardItem.name}
                description={productCardItem.description}
                price={productCardItem.price}
              />
            ))}
          </div>
        </div>
      </div>
    </>
  );
}

export default MainPage;
